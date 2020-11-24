This Visual Studio 2019 extension is the easiest way to add a consistently correct Entity Framework (EF6 or EFCore) model to your project. 

It's an opinionated code generator, adding a new file type (.efmodel) that allows for fast, easy and, most importantly, **visual** design 
of persistent classes. Inheritance, unidirectional and bidirectional associations are all supported. Enumerations are also included in 
the visual model, as is the ability to add text blocks to explain potentially arcane parts of your design.

<img src="https://msawczyn.github.io/EFDesigner/images/Designer.jpg">

While giving you complete control over how the code is generated you'll be able to create, out of the box, sophisticated, 
consistent and **correct** Entity Framework code that can be regenerated when your model changes. And, since the code is written using 
partial classes, any additions you make to your generated code are retained across subsequent generations.
The designer doesn't need to be present to use the the code that's generated - it generates standard C#, using the code-first, fluent API - so the tool doesn't
become a dependency to your project.

If you are used to the EF visual modeling that comes with Visual Studio, you'll be pretty much at home. The goal was to duplicate 
at least those features and, in addition, add all the little things that _should_ have been there. Things like:

*   importing entities from C# source, or existing DbContext definitions (including their entities) from compiled EF6 or EFCore assemblies
*   multiple views of your model to highlight important aspects of your design
*   the ability to show and hide parts of the model
*   easy customization of generated output by editing or even replacing the T4 templates
*   entities by default generated as partial classes so the generated code can be easily extended
*   class and enumeration nodes that can be colored to visually group the model
*   different concerns being generated into different subdirectories (entities, enums, dbcontext)
*   string length, index flags, required attributes and other properties being available in the designer

and many other nice-to-have bits.

For comprehensive documentation, please visit [the project's documentation site](https://msawczyn.github.io/EFDesigner/).

**ChangeLog**

**3.0**
   - **[NEW]** Support for EFCore5.X (available when selecting EFCore output with version >= 5)
      - **[NEW]** Added System.Net.IPAddress and System.Net.NetworkInformation.PhysicalAddress to the list of available property types
      - **[NEW]** Added ability to specify both default database collation and a collation override at the property level 
      - **[NEW]** Many-to-many bidirectional associations are now allowed 
      - **[NEW]** Any property type can now be used as an identity 
      - **[NEW]** Can now customize backing field names for non-AutoProperty properties 
      - **[NEW]** Properties with backing fields (i.e., non-AutoProperty properties) can now choose how EF will read/write those values (see https://docs.microsoft.com/en-us/ef/core/modeling/backing-field).
      - **[NEW]** Added support for keyless entity types created by defining queries
      - **[NEW]** Added support for keyless entity types coming from database views
   - Enhancements and Fixes
      - **[NEW]** Added ability to globally add and remove exposed foreign key properties to all modeled entities (via menu command) (see https://github.com/msawczyn/EFDesigner/issues/223)
      - **[NEW]** Added ability to choose to place newly imported model elements on the diagram where they were dropped. Caution: this can be EXTREMELY slow for large imports. (see https://github.com/msawczyn/EFDesigner/issues/225)
      - **[NEW]** Added composition and aggregation indicators to association connectors
      - Default code generation type is now the latest version of EFCore (currently, 5.0)
      - Fixed inability to paste enumerations using diagram copy/paste
      - Changing an identity property's type now changes the type of any defined foreign-key properties pointing to that identity property
      - Title text color didn't always change when class/enum fill color changed in the diagram
      - Selecting tabs or spaces for indentation in generated code has been moved to a property on the designer surface.
      - Added ModelRoot.IsEFCore5Plus convenience property. It can be used in custom T4 edits
   - Possibly breaking changes
      - T4 template structure has been changed drastically to simplify managing code generation for the various EF versions.
        If customized T4 templates have been added to a project, they'll still work, but enhancements will continue to be made only to the new, more 
        object-oriented, T4 structure. Updating the the model's .tt file to use the new template structure is quite simple; details will be in the documentation 
        at https://msawczyn.github.io/EFDesigner/Customizing.html

**2.0.5.7**
   - **[NEW]** Added ability to select tabs or spaces for indentation in generated code (Tools/Options/Entity Framework Visual Editor/Visual Editor Options) (See https://github.com/msawczyn/EFDesigner/issues/221)
   - Fixed an issue with changing visual grid size on design surface.

**2.0.5.6**
   - The project item templates for the model file had wandered away. They're back again. (See https://github.com/msawczyn/EFDesigner/issues/216)
   - Fixed a problem with existing models where class-level "AutoProperty: false" caused bad code generation. (See https://github.com/msawczyn/EFDesigner/issues/215)

**2.0.5.5**
   - Fix: Foreign key crashes when reference is on the derived table (See https://github.com/msawczyn/EFDesigner/issues/212)
   - Fixed a edge condition where an error would be thrown when deleting an association

**2.0.5.3**
   - **[NEW]** Provide option to save diagrams as uncompressed XML to facilitate version control (in Tools/Options/Entity Framework Visual Editor)
   - **[NEW]** Enhanced error reporting for assembly import errors
   - **[NEW]** Assembly import can now process assemblies with more than one DbContext class
   - Cleaned up some ambiguities in how copy/paste was handled with multiple diagrams
   - Performance improvements

**2.0.4.1**
   - **[NEW]** Added ability to hide foreign key property names on association connectors in diagrams
   - **[NEW]** Attribute glyphs (except for Warning glyphs) in diagrams are now reflected in Model Explorer
   - **[NEW]** Sped up reverse engineering a compiled assembly. As a consequence, the diagram is no longer updated when the assembly is imported (but that tended to ruin the diagram anyway)
   - Fix: Self-associations didn't appear when existing class is added to new diagram from the Model Explorer
   - Fix: Diagram no longer loses focus after its saved
   - Fix: Errors when copy/paste between diagrams in same model
   - Fix: Generalization links weren't being handled property when reverse engineering a compiled assembly
   - Fix: Under certain circumstances, declared foreign keys could erroneously be created for EF6 1-1 relationships. EF6 doesn't support this.
   - Fix: ensure glyphs in association compartments are visible
   - Fix: overly-aggressive pruning in foreign keys

**2.0.3**
   - **[NEW]** Added ability to hide foreign key property names on association connectors in diagrams
   - **[NEW]** Attribute glyphs (except for Warning glyphs) in diagrams are now reflected in Model Explorer
   - Fix: Self-associations didn't appear when existing class is added to new diagram from the Model Explorer
   - Fix: Diagram no longer loses focus after its saved
   - Fix: Errors when copy/paste between diagrams in same model

**2.0.2**
   - **[NEW]** Added count of elements in model explorer tree
   - **[NEW]** Added ability to search the model explorer for class and attribute names
   - **[NEW]** Comments can also be hidden like classes and enumerations
   - Fix: Changing String Column Name Clears Max Length Property (See https://github.com/msawczyn/EFDesigner/issues/173)
   - Fix: Dropping external files creates elements but not shapes on diagram (See https://github.com/msawczyn/EFDesigner/issues/150)
   - Fix: VS Crash (See https://github.com/msawczyn/EFDesigner/issues/177)
   - Fix: Attributes only show up on diagram where they were added (See https://github.com/msawczyn/EFDesigner/issues/179)
   - Fix: EFDesigner 2.0.1 won't create entities in the efmodel for existing poco .cs files (See https://github.com/msawczyn/EFDesigner/issues/182)

**2.0.0** 
   - **Dropped support for Visual Studio 2017**; was getting to be too much to keep the tool viable for that Visual Studio version.
   - **[NEW]** It's now possible to have multiple diagrams for the same model, each showing a different view and synchronized as the model changes. Perfect for helping to understand large models.
   - **[NEW]** Added ability to specify foreign key properties  (See https://github.com/msawczyn/EFDesigner/issues/55)
   - **[NEW]** Foreign key properties have a unique glyph so they can be easily picked out of the crowd
   - **[NEW]** Foreign key properties that are primary keys also have a unique but different glyph
   - **[NEW]** Completely restructured assembly parsers; they now cleanly handle all valid combinations of EF6/EFCore2/EFCore3 and .NETCore2/.NETCore3/.NETFramework
   - **[NEW]** Modified assembly parsers to find declared foreign keys and add them to the model appropriately
   - **[NEW]** Added options dialog (Tools/Options/Entity Framework Visual Editor)
   - **[NEW]** Added use of [GraphViz](https://www.graphviz.org/) for model layout (if installed and path is added to "Tools/Options/Entity Framework Visual Editor")
   - **[NEW]** Added switch to disable generation of classes and enumerations for those cases where they are coming from different assemblies but need to be in the model to avoid errors.
   - **[NEW]** Added visual indicator on classes and enumerations where code generation is disabled
   - **[NEW]** Added ability to override the base class of the generated DbContext to be something other than "DbContext"
   - **[NEW]** Join tables in many-to-many associations can now have custom names (EF6 only, until EFCore supports many-to-many cardinalities)
   - **[NEW]** Removing an enumeration removes all entity properties that use that enumeration, after displaying a warning.
   - **[NEW]** Designer has optional visual grid with color, size and snap-to-grid options available
   - Renamed toolbox category to "EF Model Diagrams"
   - Enhanced display of model elements in the Visual Studio property window's object list
   - Removed MSAGL layouts. No one understood them anyway.
   - Removed tool automatically installing NuGet packages. Too volatile.
   - Fix: OutputDirectory lost on reload (See https://github.com/msawczyn/EFDesigner/issues/144)
   - Fix: Unidirectional Many-to-One Association missing Required (See https://github.com/msawczyn/EFDesigner/issues/145)
   - Fix: Couldn't delete property initial value for Enum values (See https://github.com/msawczyn/EFDesigner/issues/148)
   - Fix: Support for Empty / blank "File Name Marker" (See https://github.com/msawczyn/EFDesigner/issues/149)
   - Fix: Now escaping XML comment text properly
   - Fix: Issue with GeographyPoint: System.Data.Entities.Spatial not found for .Net Core 3.1 (See https://github.com/msawczyn/EFDesigner/issues/159)
   - Fix: HasDefaultSchema doesn't work with MySql (See https://github.com/msawczyn/EFDesigner/issues/160)

[Earlier changes](https://github.com/msawczyn/EFDesigner/blob/master/changelog.txt)

A big thanks to <a href="https://www.jetbrains.com/?from=EFDesigner"><img src="https://msawczyn.github.io/EFDesigner/images/jetbrains-variant-2a.png" style="margin-bottom: -30px"></a> &nbsp; for providing free development tools to support this project.
