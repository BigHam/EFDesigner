using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
// ReSharper disable RedundantNameQualifier

namespace Sawczyn.EFDesigner.EFModel.DslPackage.TextTemplates.EditingOnly
{
   [SuppressMessage("ReSharper", "UnusedMember.Local")]
   [SuppressMessage("ReSharper", "UnusedMember.Global")]
   partial class EditOnly
   {
      #region Template
      // EFDesigner v3.0.0.0
      // Copyright (c) 2017-2020 Michael Sawczyn
      // https://github.com/msawczyn/EFDesigner

      /**************************************************
       * EFCore-specific code generation methods
       */

      void GenerateEFCore(Manager manager, ModelRoot modelRoot)
      {
         // Entities
         string fileNameMarker = string.IsNullOrEmpty(modelRoot.FileNameMarker) ? string.Empty : $".{modelRoot.FileNameMarker}";

         foreach (ModelClass modelClass in modelRoot.Classes.Where(e => e.GenerateCode && !e.IsPropertyBag))
         {
            manager.StartNewFile(Path.Combine(modelClass.EffectiveOutputDirectory, $"{modelClass.Name}{fileNameMarker}.cs"));
            WriteClass(modelClass);
         }

         // Enums

         foreach (ModelEnum modelEnum in modelRoot.Enums.Where(e => e.GenerateCode))
         {
            manager.StartNewFile(Path.Combine(modelEnum.EffectiveOutputDirectory, $"{modelEnum.Name}{fileNameMarker}.cs"));
            WriteEnum(modelEnum);
         }

         manager.StartNewFile(Path.Combine(modelRoot.ContextOutputDirectory, $"{modelRoot.EntityContainerName}{fileNameMarker}.cs"));
         WriteDbContextEFCore(modelRoot);
      }

      string[] SpatialTypesEFCore
      {
         get
         {
            return new[] {
                               "Geometry"
                             , "GeometryPoint"
                             , "GeometryLineString"
                             , "GeometryPolygon"
                             , "GeometryCollection"
                             , "GeometryMultiPoint"
                             , "GeometryMultiLineString"
                             , "GeometryMultiPolygon"
                         };
         }
      }

      List<string> GetAdditionalUsingStatementsEFCore(ModelRoot modelRoot)
      {
         List<string> result = new List<string>();
         List<string> attributeTypes = modelRoot.Classes.SelectMany(c => c.Attributes).Select(a => a.Type).Distinct().ToList();

         if (attributeTypes.Intersect(modelRoot.SpatialTypes).Any())
            result.Add("using NetTopologySuite.Geometries;");

         return result;
      }

      void WriteDbContextEFCore(ModelRoot modelRoot)
      {
         List<string> segments = new List<string>();
         ModelClass[] classesWithTables = null;

         // Note: TablePerConcreteType not yet available, but it doesn't hurt for it to be here since they shouldn't make it past the designer's validations
         switch (modelRoot.InheritanceStrategy)
         {
            case CodeStrategy.TablePerType:
               classesWithTables = modelRoot.Classes.Where(mc => (!mc.IsDependentType || !string.IsNullOrEmpty(mc.TableName)) && mc.GenerateCode).OrderBy(x => x.Name).ToArray();

               break;

            case CodeStrategy.TablePerConcreteType:
               classesWithTables = modelRoot.Classes.Where(mc => (!mc.IsDependentType || !string.IsNullOrEmpty(mc.TableName)) && !mc.IsAbstract && mc.GenerateCode).OrderBy(x => x.Name).ToArray();

               break;

            case CodeStrategy.TablePerHierarchy:
               classesWithTables = modelRoot.Classes.Where(mc => (!mc.IsDependentType || !string.IsNullOrEmpty(mc.TableName)) && mc.Superclass == null && mc.GenerateCode).OrderBy(x => x.Name).ToArray();

               break;
         }

         Output("using System;");
         Output("using System.Collections.Generic;");
         Output("using System.Linq;");
         Output("using System.ComponentModel.DataAnnotations.Schema;");
         Output("using Microsoft.EntityFrameworkCore;");
         NL();

         BeginNamespace(modelRoot.Namespace);

         WriteDbContextComments(modelRoot);

         string baseClass = string.IsNullOrWhiteSpace(modelRoot.BaseClass) ? "Microsoft.EntityFrameworkCore.DbContext" : modelRoot.BaseClass;
         Output($"{modelRoot.EntityContainerAccess.ToString().ToLower()} partial class {modelRoot.EntityContainerName} : {baseClass}");
         Output("{");

         if (classesWithTables?.Any() == true)
            WriteDbSetsEFCore(modelRoot);

         WriteConstructorsEFCore(modelRoot);
         WriteOnConfiguringEFCore(modelRoot, segments);
         WriteOnModelCreateEFCore(modelRoot, segments, classesWithTables);

         Output("}");

         EndNamespace(modelRoot.Namespace);
      }

      private void WriteDbSetsEFCore(ModelRoot modelRoot)
      {
         Output("#region DbSets");
         PluralizationService pluralizationService = ModelRoot.PluralizationService;

         foreach (ModelClass modelClass in modelRoot.Classes.Where(x => !x.IsDependentType).OrderBy(x => x.Name))
         {
            string dbSetName;

            if (!string.IsNullOrEmpty(modelClass.DbSetName))
               dbSetName = modelClass.DbSetName;
            else
            {
               dbSetName = pluralizationService?.IsSingular(modelClass.Name) == true
                              ? pluralizationService.Pluralize(modelClass.Name)
                              : modelClass.Name;
            }

            if (!string.IsNullOrEmpty(modelClass.Summary))
            {
               NL();
               Output("/// <summary>");
               WriteCommentBody($"Repository for {modelClass.FullName} - {modelClass.Summary}");
               Output("/// </summary>");
            }

            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (modelClass.IsPropertyBag)
               Output($"{modelRoot.DbSetAccess.ToString().ToLower()} virtual Microsoft.EntityFrameworkCore.DbSet<Dictionary<string, object>> {dbSetName} => Set<Dictionary<string, object>>(\"{modelClass.Name}\");");
            else
               Output($"{modelRoot.DbSetAccess.ToString().ToLower()} virtual Microsoft.EntityFrameworkCore.DbSet<{modelClass.FullName}> {dbSetName} {{ get; set; }}");
         }

         Output("#endregion DbSets");
         NL();
      }

      private void WriteConstructorsEFCore(ModelRoot modelRoot)
      {
         if (!string.IsNullOrEmpty(modelRoot.ConnectionString) || !string.IsNullOrEmpty(modelRoot.ConnectionStringName))
         {
            string connectionString = string.IsNullOrEmpty(modelRoot.ConnectionString)
                                    ? $"Name={modelRoot.ConnectionStringName}"
                                    : modelRoot.ConnectionString;

            Output("/// <summary>");
            Output("/// Default connection string");
            Output("/// </summary>");
            Output($"public static string ConnectionString {{ get; set; }} = @\"{connectionString}\";");
            NL();
         }

         Output("/// <inheritdoc />");
         Output($"public {modelRoot.EntityContainerName}(DbContextOptions<{modelRoot.EntityContainerName}> options) : base(options)");
         Output("{");
         Output("}");
         NL();

         Output("partial void CustomInit(DbContextOptionsBuilder optionsBuilder);");
         NL();
      }

      private void WriteOnConfiguringEFCore(ModelRoot modelRoot, List<string> segments)
      {
         Output("/// <inheritdoc />");
         Output("protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)");
         Output("{");

         segments.Clear();

         if (modelRoot.GetEntityFrameworkPackageVersionNum() >= 2.1 && modelRoot.LazyLoadingEnabled)
            segments.Add("UseLazyLoadingProxies()");

         if (segments.Any())
         {
            segments.Insert(0, "optionsBuilder");

            Output(modelRoot, segments);
            NL();
         }

         Output("CustomInit(optionsBuilder);");
         Output("}");
         NL();
      }

      private void WriteOnModelCreateEFCore(ModelRoot modelRoot, List<string> segments, ModelClass[] classesWithTables)
      {
         Output("partial void OnModelCreatingImpl(ModelBuilder modelBuilder);");
         Output("partial void OnModelCreatedImpl(ModelBuilder modelBuilder);");
         NL();

         Output("/// <inheritdoc />");
         Output("protected override void OnModelCreating(ModelBuilder modelBuilder)");
         Output("{");
         Output("base.OnModelCreating(modelBuilder);");
         Output("OnModelCreatingImpl(modelBuilder);");
         NL();

         if (!string.IsNullOrEmpty(modelRoot.DatabaseSchema))
            Output($"modelBuilder.HasDefaultSchema(\"{modelRoot.DatabaseSchema}\");");

         List<Association> visited = new List<Association>();
         List<string> foreignKeyColumns = new List<string>();

         foreach (ModelClass modelClass in modelRoot.Classes.OrderBy(x => x.Name))
         {
            segments.Clear();
            foreignKeyColumns.Clear();
            NL();

            if (modelClass.IsPropertyBag)
               WritePropertyBagClassBuilder(modelRoot, segments, classesWithTables, modelClass, visited, foreignKeyColumns);
            else
               WriteStandardClassBuilder(modelRoot, segments, classesWithTables, modelClass, visited, foreignKeyColumns);
         }

         NL();

         Output("OnModelCreatedImpl(modelBuilder);");
         Output("}");
      }

      private void WriteStandardClassBuilder(ModelRoot modelRoot, List<string> segments, ModelClass[] classesWithTables, ModelClass modelClass, List<Association> visited, List<string> foreignKeyColumns)
      {
         // class level
         //if (modelClass.IsDependentType)
         //   segments.Add($"modelBuilder.Owned<{modelClass.FullName}>()");
         //else
         {
            segments.Add($"modelBuilder.Entity<{modelClass.FullName}>()");

            foreach (ModelAttribute transient in modelClass.Attributes.Where(x => !x.Persistent))
               segments.Add($"Ignore(t => t.{transient.Name})");

            // note: this must come before the 'ToTable' call or there's a runtime error
            if (modelRoot.InheritanceStrategy == CodeStrategy.TablePerConcreteType && modelClass.Superclass != null)
               segments.Add("Map(x => x.MapInheritedProperties())");
         }

         if (classesWithTables.Contains(modelClass))
         {
            if (modelClass.IsQueryType)
            {
               Output($"// There is no table defined for {modelClass.Name} because its IsQueryType value is");
               Output($"// set to 'true'. Please provide the {modelRoot.FullName}.Get{modelClass.Name}SqlQuery() method in the partial class.");
               Output($"// ");
               Output($"// private string Get{modelClass.Name}SqlQuery()");
               Output("// {");
               Output($"//    return the defining SQL query that pulls all the properties for {modelClass.FullName}");
               Output("// }");

               segments.Add($"ToSqlQuery(Get{modelClass.Name}SqlQuery())");
            }
            else //if (!modelClass.IsDependentType)
            {
               if (!string.IsNullOrEmpty(modelClass.TableName))
               {
                  segments.Add(string.IsNullOrEmpty(modelClass.DatabaseSchema) || modelClass.DatabaseSchema == modelClass.ModelRoot.DatabaseSchema
                                  ? $"ToTable(\"{modelClass.TableName}\")"
                                  : $"ToTable(\"{modelClass.TableName}\", \"{modelClass.DatabaseSchema}\")");
               }

               // primary key code segments must be output last, since HasKey returns a different type
               List<ModelAttribute> identityAttributes = modelClass.IdentityAttributes.ToList();

               if (identityAttributes.Count == 1)
                  segments.Add($"HasKey(t => t.{identityAttributes[0].Name})");
               else if (identityAttributes.Count > 1)
                  segments.Add($"HasKey(t => new {{ t.{string.Join(", t.", identityAttributes.Select(ia => ia.Name))} }})");
            }
         }

         if (segments.Count > 1 || modelClass.IsDependentType)
            Output(modelRoot, segments);

         //if (!modelClass.IsDependentType)
         {
            // attribute level
            foreach (ModelAttribute modelAttribute in modelClass.Attributes.Where(x => x.Persistent && !SpatialTypesEFCore.Contains(x.Type)))
            {
               segments.Clear();

               if ((modelAttribute.MaxLength ?? 0) > 0)
                  segments.Add($"HasMaxLength({modelAttribute.MaxLength.Value})");

               if (modelAttribute.Required)
                  segments.Add("IsRequired()");

               if (modelAttribute.ColumnName != modelAttribute.Name && !string.IsNullOrEmpty(modelAttribute.ColumnName))
                  segments.Add($"HasColumnName(\"{modelAttribute.ColumnName}\")");

               if (!modelAttribute.AutoProperty)
               {
                  segments.Add($"HasField(\"{modelAttribute.BackingFieldName}\")");
                  segments.Add($"UsePropertyAccessMode(PropertyAccessMode.{modelAttribute.PropertyAccessMode})");
               }

               if (!string.IsNullOrEmpty(modelAttribute.ColumnType) && modelAttribute.ColumnType.ToLowerInvariant() != "default")
               {
                  if (modelAttribute.ColumnType.ToLowerInvariant() == "varchar" || modelAttribute.ColumnType.ToLowerInvariant() == "nvarchar" || modelAttribute.ColumnType.ToLowerInvariant() == "char")
                     segments.Add($"HasColumnType(\"{modelAttribute.ColumnType}({(modelAttribute.MaxLength > 0 ? modelAttribute.MaxLength.ToString() : "max")})\")");
                  else
                     segments.Add($"HasColumnType(\"{modelAttribute.ColumnType}\")");
               }

               if (!string.IsNullOrEmpty(modelAttribute.InitialValue) && modelRoot.IsEFCore5Plus)
               {
                  string initialValue = modelAttribute.InitialValue;

                  // using switch statements since more exceptions will undoubtedly be created in the future
                  switch (modelAttribute.Type)
                  {
                     case "DateTime":
                        switch (modelAttribute.InitialValue)
                        {
                           case "DateTime.Now":
                              segments.Add("HasDefaultValue(DateTime.Now)");

                              //segments.Add("HasDefaultValueSql(\"getdate()\")");
                              break;

                           case "DateTime.UtcNow":
                              segments.Add("HasDefaultValue(DateTime.UtcNow)");

                              //segments.Add("HasDefaultValueSql(\"getutcdate()\")");
                              break;

                           default:
                              if (!initialValue.StartsWith("\""))
                                 initialValue = "\"" + initialValue;

                              if (!initialValue.EndsWith("\""))
                                 initialValue = initialValue + "\"";

                              segments.Add($"HasDefaultValue(DateTime.Parse({initialValue}))");
                              break;
                        }

                        break;

                     case "String":
                        if (!initialValue.StartsWith("\""))
                           initialValue = "\"" + initialValue;

                        if (!initialValue.EndsWith("\""))
                           initialValue = initialValue + "\"";

                        segments.Add($"HasDefaultValue({initialValue})");
                        break;

                     default:
                        segments.Add($"HasDefaultValue({modelAttribute.InitialValue})");
                        break;
                  }
               }

               if (modelAttribute.IsConcurrencyToken)
                  segments.Add("IsRowVersion()");

               if (modelAttribute.IsIdentity)
               {
                  segments.Add(modelAttribute.IdentityType == IdentityType.AutoGenerated
                                  ? "ValueGeneratedOnAdd()"
                                  : "ValueGeneratedNever()");
               }

               if (segments.Any())
               {
                  segments.Insert(0, $"modelBuilder.Entity<{modelClass.FullName}>()");
                  segments.Insert(1, $"Property(t => t.{modelAttribute.Name})");

                  Output(modelRoot, segments);
               }

               if (modelAttribute.Indexed && !modelAttribute.IsIdentity)
               {
                  segments.Clear();

                  segments.Add($"modelBuilder.Entity<{modelClass.FullName}>().HasIndex(t => t.{modelAttribute.Name})");

                  if (modelAttribute.IndexedUnique)
                     segments.Add("IsUnique()");

                  Output(modelRoot, segments);
               }
            }
         }

         bool hasDefinedConcurrencyToken = modelClass.AllAttributes.Any(x => x.IsConcurrencyToken);

         if (!hasDefinedConcurrencyToken && modelClass.EffectiveConcurrency == ConcurrencyOverride.Optimistic)
            Output($@"modelBuilder.Entity<{modelClass.FullName}>().Property<byte[]>(""Timestamp"").IsConcurrencyToken();");

         // Navigation endpoints are distingished as Source and Target. They are also distinguished as Principal
         // and Dependent. How do these map? Short answer: they don't. Source and Target are accidents of where the user started drawing the association.

         // What matters is the Principal and Dependent classifications, so we look at those. 
         // In the case of one-to-one or zero-to-one-to-zero-to-one, it's model dependent and the user has to tell us
         // In all other cases, we can tell by the cardinalities of the associations

         // navigation properties
         List<string> declaredShadowProperties = new List<string>();
         DefineEFCoreUnidirectionalAssociations(modelClass, visited, segments, foreignKeyColumns, declaredShadowProperties);
         DefineEFCoreBidirectionalAssociations(modelClass, visited, segments, foreignKeyColumns, declaredShadowProperties);
      }

      private void DefineEFCoreUnidirectionalAssociations(ModelClass modelClass, 
                                                          List<Association> visited, 
                                                          List<string> segments, 
                                                          List<string> foreignKeyColumns,
                                                          List<string> declaredShadowProperties)
      {
         ModelRoot modelRoot = modelClass.ModelRoot;

         // ReSharper disable once LoopCanBePartlyConvertedToQuery
         foreach (UnidirectionalAssociation association in Association.GetLinksToTargets(modelClass)
                                                                      .OfType<UnidirectionalAssociation>()
                                                                      .Where(x => x.Persistent && !x.Target.IsDependentType))
         {
            if (visited.Contains(association))
               continue;

            visited.Add(association);

            bool required = false;

            segments.Clear();
            segments.Add($"modelBuilder.Entity<{modelClass.FullName}>()");

            switch (association.TargetMultiplicity) // realized by property on source
            {
               case Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany:
                  segments.Add($"HasMany(x => x.{association.TargetPropertyName})");

                  break;

               case Sawczyn.EFDesigner.EFModel.Multiplicity.One:
                  segments.Add($"HasOne(x => x.{association.TargetPropertyName})");
                  required = (modelClass == association.Principal);

                  break;

               case Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroOne:
                  segments.Add($"HasOne(x => x.{association.TargetPropertyName})");

                  break;
            }

            switch (association.SourceMultiplicity) // realized by property on target, but no property on target
            {
               case Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany:

                  if (association.TargetMultiplicity == Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany)
                  {
                     string tableMap = string.IsNullOrEmpty(association.JoinTableName) ? $"{association.Source.Name}_x_{association.TargetPropertyName}" : association.JoinTableName;
                     segments.Add($"WithMany(\"{tableMap}\")"); // workaround for lack of CollectionNavigationBuilder.WithMany(). Tracked by https://github.com/dotnet/efcore/issues/3864 
                     segments.Add($"UsingEntity(x => x.ToTable(\"{tableMap}\"))");
                  }
                  else
                     segments.Add("WithMany()");

                  break;

               case Sawczyn.EFDesigner.EFModel.Multiplicity.One:
                  segments.Add("WithOne()");
                  required = (modelClass == association.Principal);

                  break;

               case Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroOne:
                  segments.Add("WithOne()");

                  break;
            }

            string foreignKeySegment = CreateForeignKeySegmentEFCore(association, foreignKeyColumns);
            if (!string.IsNullOrEmpty(foreignKeySegment))
                segments.Add(foreignKeySegment);

            if (required && (modelClass.ModelRoot.IsEFCore5Plus || association.SourceMultiplicity != Sawczyn.EFDesigner.EFModel.Multiplicity.One || association.TargetMultiplicity != Sawczyn.EFDesigner.EFModel.Multiplicity.One))
                segments.Add("IsRequired()");

            if ((association.TargetRole == EndpointRole.Principal || association.SourceRole == EndpointRole.Principal) && !association.LinksDependentType)
            {
               DeleteAction deleteAction = association.SourceRole == EndpointRole.Principal
                                              ? association.SourceDeleteAction
                                              : association.TargetDeleteAction;

               switch (deleteAction)
               {
                  case DeleteAction.None:
                     segments.Add("OnDelete(DeleteBehavior.Restrict)");

                     break;

                  case DeleteAction.Cascade:
                     segments.Add("OnDelete(DeleteBehavior.Cascade)");

                     break;
               }
            }

            Output(modelRoot, segments);

            if (modelClass.ModelRoot.IsEFCore5Plus && association.SourceMultiplicity == Sawczyn.EFDesigner.EFModel.Multiplicity.One && association.TargetMultiplicity == Sawczyn.EFDesigner.EFModel.Multiplicity.One)
                Output($"modelBuilder.Entity<{modelClass.FullName}>().Navigation(x => x.{association.TargetPropertyName}).IsRequired();");
         }
      }

      private void DefineEFCoreBidirectionalAssociations(ModelClass modelClass, 
                                                         List<Association> visited, 
                                                         List<string> segments, 
                                                         List<string> foreignKeyColumns,
                                                         List<string> declaredShadowProperties)
      {
         ModelRoot modelRoot = modelClass.ModelRoot;

         // ReSharper disable once LoopCanBePartlyConvertedToQuery
         foreach (BidirectionalAssociation association in Association.GetLinksToTargets(modelClass)
                                                                      .OfType<BidirectionalAssociation>()
                                                                      .Where(x => x.Persistent && !x.Target.IsDependentType))
         {
            if (visited.Contains(association))
               continue;

            visited.Add(association);

            bool required = false;

            segments.Clear();
            segments.Add($"modelBuilder.Entity<{modelClass.FullName}>()");

            switch (association.TargetMultiplicity) // realized by property on source
            {
               case Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany:
                  segments.Add($"HasMany(x => x.{association.TargetPropertyName})");

                  break;

               case Sawczyn.EFDesigner.EFModel.Multiplicity.One:
                  segments.Add($"HasOne(x => x.{association.TargetPropertyName})");
                  required = (modelClass == association.Principal);

                  break;

               case Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroOne:
                  segments.Add($"HasOne(x => x.{association.TargetPropertyName})");

                  break;
            }

            switch (association.SourceMultiplicity) // realized by property on target, but no property on target
            {
               case Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany:
                  segments.Add($"WithMany(x => x.{association.SourcePropertyName})"); 

                  if (association.TargetMultiplicity == Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany)
                  {
                     string tableMap = string.IsNullOrEmpty(association.JoinTableName) ? $"{association.TargetPropertyName}_x_{association.TargetPropertyName}" : association.JoinTableName;
                     segments.Add($"UsingEntity(x => x.ToTable(\"{tableMap}\"))");
                  }

                  break;

               case Sawczyn.EFDesigner.EFModel.Multiplicity.One:
                  segments.Add($"WithOne(x => x.{association.SourcePropertyName})");
                  required = (modelClass == association.Principal);

                  break;

               case Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroOne:
                  segments.Add($"WithOne(x => x.{association.SourcePropertyName})");

                  break;
            }

            string foreignKeySegment = CreateForeignKeySegmentEFCore(association, foreignKeyColumns);
            if (!string.IsNullOrEmpty(foreignKeySegment))
                segments.Add(foreignKeySegment);

            if (required && (modelClass.ModelRoot.IsEFCore5Plus || association.SourceMultiplicity != Sawczyn.EFDesigner.EFModel.Multiplicity.One || association.TargetMultiplicity != Sawczyn.EFDesigner.EFModel.Multiplicity.One))
                segments.Add("IsRequired()");

            if ((association.TargetRole == EndpointRole.Principal || association.SourceRole == EndpointRole.Principal) && !association.LinksDependentType)
            {
               DeleteAction deleteAction = association.SourceRole == EndpointRole.Principal
                                              ? association.SourceDeleteAction
                                              : association.TargetDeleteAction;

               switch (deleteAction)
               {
                  case DeleteAction.None:
                     segments.Add("OnDelete(DeleteBehavior.Restrict)");

                     break;

                  case DeleteAction.Cascade:
                     segments.Add("OnDelete(DeleteBehavior.Cascade)");

                     break;
               }
            }

            Output(modelRoot, segments);

            if (modelClass.ModelRoot.IsEFCore5Plus && association.SourceMultiplicity == Sawczyn.EFDesigner.EFModel.Multiplicity.One && association.TargetMultiplicity == Sawczyn.EFDesigner.EFModel.Multiplicity.One)
                Output($"modelBuilder.Entity<{modelClass.FullName}>().Navigation(x => x.{association.TargetPropertyName}).IsRequired();");
         }
      }

      private void WritePropertyBagClassBuilder(ModelRoot modelRoot, List<string> segments, ModelClass[] classesWithTables, ModelClass modelClass, List<Association> visited, List<string> foreignKeyColumns)
      {
         // class level

         segments.Add($"modelBuilder.{(modelClass.IsDependentType ? "Owned" : "Entity")}<{modelClass.FullName}>()");

         foreach (ModelAttribute transient in modelClass.Attributes.Where(x => !x.Persistent))
            segments.Add($"Ignore(t => t.{transient.Name})");

         if (!modelClass.IsDependentType)
         {
            // note: this must come before the 'ToTable' call or there's a runtime error
            if (modelRoot.InheritanceStrategy == CodeStrategy.TablePerConcreteType && modelClass.Superclass != null)
               segments.Add("Map(x => x.MapInheritedProperties())");

            if (classesWithTables.Contains(modelClass))
            {
               segments.Add(string.IsNullOrEmpty(modelClass.DatabaseSchema) || modelClass.DatabaseSchema == modelClass.ModelRoot.DatabaseSchema
                               ? $"ToTable(\"{modelClass.TableName}\")"
                               : $"ToTable(\"{modelClass.TableName}\", \"{modelClass.DatabaseSchema}\")");

               // primary key code segments must be output last, since HasKey returns a different type
               List<ModelAttribute> identityAttributes = modelClass.IdentityAttributes.ToList();

               if (identityAttributes.Count == 1)
                  segments.Add($"HasKey(t => t.{identityAttributes[0].Name})");
               else if (identityAttributes.Count > 1)
                  segments.Add($"HasKey(t => new {{ t.{string.Join(", t.", identityAttributes.Select(ia => ia.Name))} }})");
            }
         }

         if (segments.Count > 1 || modelClass.IsDependentType)
            Output(modelRoot, segments);

         // attribute level
         foreach (ModelAttribute modelAttribute in modelClass.Attributes.Where(x => x.Persistent && !SpatialTypesEFCore.Contains(x.Type)))
         {
            segments.Clear();

            if ((modelAttribute.MaxLength ?? 0) > 0)
               segments.Add($"HasMaxLength({modelAttribute.MaxLength.Value})");

            if (modelAttribute.Required)
               segments.Add("IsRequired()");

            if (modelAttribute.ColumnName != modelAttribute.Name && !string.IsNullOrEmpty(modelAttribute.ColumnName))
               segments.Add($"HasColumnName(\"{modelAttribute.ColumnName}\")");

            if (!modelAttribute.AutoProperty)
            {
               segments.Add($"HasField(\"{modelAttribute.BackingFieldName}\")");
               segments.Add($"UsePropertyAccessMode(PropertyAccessMode.{modelAttribute.PropertyAccessMode})");
            }

            if (!string.IsNullOrEmpty(modelAttribute.ColumnType) && modelAttribute.ColumnType.ToLowerInvariant() != "default")
            {
               if (modelAttribute.ColumnType.ToLowerInvariant() == "varchar" || modelAttribute.ColumnType.ToLowerInvariant() == "nvarchar" || modelAttribute.ColumnType.ToLowerInvariant() == "char")
                  segments.Add($"HasColumnType(\"{modelAttribute.ColumnType}({(modelAttribute.MaxLength > 0 ? modelAttribute.MaxLength.ToString() : "max")})\")");
               else
                  segments.Add($"HasColumnType(\"{modelAttribute.ColumnType}\")");
            }

            if (!string.IsNullOrEmpty(modelAttribute.InitialValue) && modelRoot.IsEFCore5Plus)
            {
               string initialValue = modelAttribute.InitialValue;

               // using switch statements since more exceptions will undoubtedly be created in the future
               switch (modelAttribute.Type)
               {
                  case "DateTime":
                     switch (modelAttribute.InitialValue)
                     {
                        case "DateTime.Now":
                           segments.Add("HasDefaultValue(DateTime.Now)");

                           //segments.Add("HasDefaultValueSql(\"getdate()\")");
                           break;

                        case "DateTime.UtcNow":
                           segments.Add("HasDefaultValue(DateTime.UtcNow)");

                           //segments.Add("HasDefaultValueSql(\"getutcdate()\")");
                           break;

                        default:
                           if (!initialValue.StartsWith("\""))
                              initialValue = "\"" + initialValue;

                           if (!initialValue.EndsWith("\""))
                              initialValue = initialValue + "\"";

                           segments.Add($"HasDefaultValue(DateTime.Parse({initialValue}))");
                           break;
                     }

                     break;

                  case "String":
                     if (!initialValue.StartsWith("\""))
                        initialValue = "\"" + initialValue;

                     if (!initialValue.EndsWith("\""))
                        initialValue = initialValue + "\"";

                     segments.Add($"HasDefaultValue({initialValue})");
                     break;

                  default:
                     segments.Add($"HasDefaultValue({modelAttribute.InitialValue})");
                     break;
               }
            }

            if (modelAttribute.IsConcurrencyToken)
               segments.Add("IsRowVersion()");

            if (modelAttribute.IsIdentity)
            {
               segments.Add(modelAttribute.IdentityType == IdentityType.AutoGenerated
                               ? "ValueGeneratedOnAdd()"
                               : "ValueGeneratedNever()");
            }

            if (segments.Any())
            {
               segments.Insert(0, $"modelBuilder.{(modelClass.IsDependentType ? "Owned" : "Entity")}<{modelClass.FullName}>()");
               segments.Insert(1, $"Property(t => t.{modelAttribute.Name})");

               Output(modelRoot, segments);
            }

            if (modelAttribute.Indexed && !modelAttribute.IsIdentity)
            {
               segments.Clear();

               segments.Add($"modelBuilder.Entity<{modelClass.FullName}>().HasIndex(t => t.{modelAttribute.Name})");

               if (modelAttribute.IndexedUnique)
                  segments.Add("IsUnique()");

               Output(modelRoot, segments);
            }
         }

         bool hasDefinedConcurrencyToken = modelClass.AllAttributes.Any(x => x.IsConcurrencyToken);

         if (!hasDefinedConcurrencyToken && modelClass.EffectiveConcurrency == ConcurrencyOverride.Optimistic)
            Output($@"modelBuilder.Entity<{modelClass.FullName}>().Property<byte[]>(""Timestamp"").IsConcurrencyToken();");

         // Navigation endpoints are distingished as Source and Target. They are also distinguished as Principal
         // and Dependent. How do these map? Short answer: they don't. Source and Target are accidents of where the user started drawing the association.

         // What matters is the Principal and Dependent classifications, so we look at those. 
         // In the case of one-to-one or zero-to-one-to-zero-to-one, it's model dependent and the user has to tell us
         // In all other cases, we can tell by the cardinalities of the associations

         // navigation properties

         // ReSharper disable once LoopCanBePartlyConvertedToQuery
         foreach (UnidirectionalAssociation association in Association.GetLinksToTargets(modelClass)
                                                                      .OfType<UnidirectionalAssociation>()
                                                                      .Where(x => x.Persistent))
         {
            if (visited.Contains(association))
               continue;

            visited.Add(association);

            segments.Clear();
            segments.Add($"modelBuilder.Entity<{modelClass.FullName}>()");
            bool required = false;

            switch (association.TargetMultiplicity) // realized by property on source
            {
               case Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany:
                  if (modelRoot.IsEFCore5Plus || association.SourceMultiplicity != Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany)
                     segments.Add($"HasMany(x => x.{association.TargetPropertyName})");
                  else
                     continue;

                  break;

               case Sawczyn.EFDesigner.EFModel.Multiplicity.One:
                  segments.Add($"HasOne(x => x.{association.TargetPropertyName})");
                  required = (modelClass == association.Principal);

                  break;

               case Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroOne:
                  segments.Add($"HasOne(x => x.{association.TargetPropertyName})");

                  break;

                  //case Sawczyn.EFDesigner.EFModel.Multiplicity.OneMany:
                  //   segments.Add($"HasMany(x => x.{association.TargetPropertyName})");
                  //   break;
            }

            string columnPrefix = association.SourceRole == EndpointRole.Dependent
                                     ? ""
                                     : association.Target.Name + "_";

            switch (association.SourceMultiplicity) // realized by shadow property on target
            {
               case Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany:
                  if (modelRoot.IsEFCore5Plus || association.TargetMultiplicity != Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany)
                  {
                     segments.Add("WithMany()");

                     //segments.Add($"HasForeignKey(\"{columnPrefix}{association.TargetPropertyName}_Id\")");
                  }
                  else
                     continue;

                  break;

               case Sawczyn.EFDesigner.EFModel.Multiplicity.One:
                  segments.Add("WithOne()");

                  segments.Add(association.TargetMultiplicity != Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany
                                  ? $"HasForeignKey<{association.Source.FullName}>(\"{columnPrefix}{association.TargetPropertyName}_Id\")"
                                  : $"HasForeignKey(\"{columnPrefix}{association.TargetPropertyName}_Id\")");

                  required = (modelClass == association.Principal);

                  break;

               case Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroOne:
                  segments.Add("WithOne()");

                  segments.Add(association.TargetMultiplicity != Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany
                                  ? $"HasForeignKey<{association.Source.FullName}>(\"{columnPrefix}{association.TargetPropertyName}_Id\")"
                                  : $"HasForeignKey(\"{columnPrefix}{association.TargetPropertyName}_Id\")");

                  break;

                  //case Sawczyn.EFDesigner.EFModel.Multiplicity.OneMany:
                  //   segments.Add("HasMany()");
                  //   break;
            }

            if (required)
               segments.Add("IsRequired()");

            if (association.TargetRole == EndpointRole.Principal || association.SourceRole == EndpointRole.Principal)
            {
               DeleteAction deleteAction = association.SourceRole == EndpointRole.Principal
                                              ? association.SourceDeleteAction
                                              : association.TargetDeleteAction;

               switch (deleteAction)
               {
                  case DeleteAction.None:
                     segments.Add("OnDelete(DeleteBehavior.Restrict)");

                     break;

                  case DeleteAction.Cascade:
                     segments.Add("OnDelete(DeleteBehavior.Cascade)");

                     break;
               }
            }

            Output(modelRoot, segments);

            if (!association.TargetAutoProperty && association.TargetMultiplicity != Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany)
            {
               segments.Clear();

               if (modelRoot.IsEFCore5Plus)
               {
                  segments.Add($"modelBuilder.Entity<{modelClass.FullName}>().Navigation(x => x.{association.TargetPropertyName})");
                  segments.Add($"HasField(\"{association.TargetBackingFieldName}\")");
                  segments.Add($"UsePropertyAccessMode(PropertyAccessMode.{association.TargetPropertyAccessMode})");

                  Output(modelRoot, segments);
                  segments.Clear();
               }
               else
               {
                  segments.Add($"modelBuilder.Entity<{modelClass.FullName}>().Metadata.FindNavigation(nameof({modelClass.FullName}.{association.TargetPropertyName}))");
                  segments.Add($"SetField(\"{association.TargetBackingFieldName}\")");

                  Output(modelRoot, segments);
                  segments.Clear();

                  segments.Add($"modelBuilder.Entity<{modelClass.FullName}>().Metadata.FindNavigation(nameof({modelClass.FullName}.{association.TargetPropertyName}))");
                  segments.Add($"SetPropertyAccessMode(PropertyAccessMode.{association.TargetPropertyAccessMode})");

                  Output(modelRoot, segments);
                  segments.Clear();
               }
            }
         }

         // ReSharper disable once LoopCanBePartlyConvertedToQuery
         foreach (BidirectionalAssociation association in Association.GetLinksToSources(modelClass)
                                                                     .OfType<BidirectionalAssociation>()
                                                                     .Where(x => x.Persistent))
         {
            if (visited.Contains(association))
               continue;

            visited.Add(association);

            // TODO: fix cascade delete
            bool required = false;

            segments.Clear();
            segments.Add($"modelBuilder.Entity<{modelClass.FullName}>()");

            switch (association.SourceMultiplicity) // realized by property on target
            {
               case Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany:
                  if (modelRoot.IsEFCore5Plus || association.TargetMultiplicity != Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany)
                     segments.Add($"HasMany(x => x.{association.SourcePropertyName})");
                  else
                     continue;

                  break;

               case Sawczyn.EFDesigner.EFModel.Multiplicity.One:
                  segments.Add($"HasOne(x => x.{association.SourcePropertyName})");
                  required = (modelClass == association.Principal);

                  break;

               case Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroOne:
                  segments.Add($"HasOne(x => x.{association.SourcePropertyName})");

                  break;

                  //case Sawczyn.EFDesigner.EFModel.Multiplicity.OneMany:
                  //   segments.Add($"HasMany(x => x.{association.SourcePropertyName})");
                  //   break;
            }

            switch (association.TargetMultiplicity) // realized by property on source
            {
               case Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany:
                  if (modelRoot.IsEFCore5Plus || association.SourceMultiplicity != Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany)
                     segments.Add($"WithMany(x => x.{association.TargetPropertyName})");
                  else
                     continue;

                  break;

               case Sawczyn.EFDesigner.EFModel.Multiplicity.One:
                  segments.Add($"WithOne(x => x.{association.TargetPropertyName})");
                  required = (modelClass == association.Principal);

                  break;

               case Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroOne:
                  segments.Add($"WithOne(x => x.{association.TargetPropertyName})");

                  break;

                  //case Sawczyn.EFDesigner.EFModel.Multiplicity.OneMany:
                  //   segments.Add($"HasMany(x => x.{association.TargetPropertyName})");
                  //   break;
            }

            string foreignKeySegment = CreateForeignKeySegmentEFCore(association, foreignKeyColumns);

            if (foreignKeySegment != null)
               segments.Add(foreignKeySegment);

            if (required)
               segments.Add("IsRequired()");

            if (association.TargetRole == EndpointRole.Principal || association.SourceRole == EndpointRole.Principal)
            {
               DeleteAction deleteAction = association.SourceRole == EndpointRole.Principal
                                              ? association.SourceDeleteAction
                                              : association.TargetDeleteAction;

               switch (deleteAction)
               {
                  case DeleteAction.None:
                     segments.Add("OnDelete(DeleteBehavior.Restrict)");

                     break;

                  case DeleteAction.Cascade:
                     segments.Add("OnDelete(DeleteBehavior.Cascade)");

                     break;
               }
            }

            Output(modelRoot, segments);
         }
      }

      string CreateForeignKeySegmentEFCore(Association association, List<string> foreignKeyColumns)
      {
         // foreign key definitions always go in the table representing the Dependent end of the association
         // if there is no dependent end (i.e., many-to-many), there are no foreign keys
         ModelClass principal;
         ModelClass dependent;

         if (association.SourceRole == EndpointRole.Dependent)
         {
            dependent = association.Source;
            principal = association.Target;
         }
         else if (association.TargetRole == EndpointRole.Dependent)
         {
            dependent = association.Target;
            principal = association.Source;
         }
         else
            return null;

         string columnName;

         if (string.IsNullOrWhiteSpace(association.FKPropertyName))
         {
            // shadow properties
            columnName = string.Join(", ", principal.IdentityAttributes
                                                    .Select(a => CreateShadowPropertyName(association, foreignKeyColumns, a))
                                                    .Select(s => $@"""{s.Trim()}"""));
         }
         else
         {
            // defined properties
            foreignKeyColumns.AddRange(association.FKPropertyName.Split(','));
            columnName = string.Join(", ", association.FKPropertyName.Split(',').Select(s => $@"""{s.Trim()}"""));
         }

         if (string.IsNullOrEmpty(columnName))
            return null;

         bool useGeneric = association.SourceMultiplicity != Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany && 
                           association.TargetMultiplicity != Sawczyn.EFDesigner.EFModel.Multiplicity.ZeroMany;

         return useGeneric
                     ? $"HasForeignKey<{dependent.FullName}>({columnName})"
                     : $"HasForeignKey({columnName})";
      }
      #endregion Template      
   }
}



