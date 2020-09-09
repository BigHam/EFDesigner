//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
//
//     Produced by Entity Framework Visual Editor v2.1.0.0
//     Source:                    https://github.com/msawczyn/EFDesigner
//     Visual Studio Marketplace: https://marketplace.visualstudio.com/items?itemName=michaelsawczyn.EFDesigner
//     Documentation:             https://msawczyn.github.io/EFDesigner/
//     License (MIT):             https://github.com/msawczyn/EFDesigner/blob/master/LICENSE
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Testing
{
   public partial class UParentRequired
   {
      partial void Init();

      /// <summary>
      /// Default constructor
      /// </summary>
      public UParentRequired()
      {
         UChildCollection = new System.Collections.Generic.HashSet<global::Testing.UChild>();

         Init();
      }

      /*************************************************************************
       * Properties
       *************************************************************************/

      /// <summary>
      /// Backing field for Id
      /// </summary>
      internal int _id;
      /// <summary>
      /// When provided in a partial class, allows value of Id to be changed before setting.
      /// </summary>
      partial void SetId(int oldValue, ref int newValue);
      /// <summary>
      /// When provided in a partial class, allows value of Id to be changed before returning.
      /// </summary>
      partial void GetId(ref int result);

      /// <summary>
      /// Identity, Indexed, Required
      /// </summary>
      [Key]
      [Required]
      public int Id
      {
         get
         {
            int value = _id;
            GetId(ref value);
            return (_id = value);
         }
         protected set
         {
            int oldValue = _id;
            SetId(oldValue, ref value);
            if (oldValue != value)
            {
               _id = value;
            }
         }
      }

      /// <summary>
      /// Backing field for Property1ab
      /// </summary>
      protected string _property1ab;
      /// <summary>
      /// When provided in a partial class, allows value of Property1ab to be changed before setting.
      /// </summary>
      partial void SetProperty1ab(string oldValue, ref string newValue);
      /// <summary>
      /// When provided in a partial class, allows value of Property1ab to be changed before returning.
      /// </summary>
      partial void GetProperty1ab(ref string result);

      public string Property1ab
      {
         get
         {
            string value = _property1ab;
            GetProperty1ab(ref value);
            return (_property1ab = value);
         }
         set
         {
            string oldValue = _property1ab;
            SetProperty1ab(oldValue, ref value);
            if (oldValue != value)
            {
               _property1ab = value;
            }
         }
      }

      /*************************************************************************
       * Navigation properties
       *************************************************************************/

      protected global::Testing.UChild _uChildRequired;
      partial void SetUChildRequired(global::Testing.UChild oldValue, ref global::Testing.UChild newValue);
      partial void GetUChildRequired(ref global::Testing.UChild result);

      /// <summary>
      /// Required
      /// </summary>
      public virtual global::Testing.UChild UChildRequired
      {
         get
         {
            global::Testing.UChild value = _uChildRequired;
            GetUChildRequired(ref value);
            return (_uChildRequired = value);
         }
         set
         {
            global::Testing.UChild oldValue = _uChildRequired;
            SetUChildRequired(oldValue, ref value);
            if (oldValue != value)
            {
               _uChildRequired = value;
            }
         }
      }

      public virtual ICollection<global::Testing.UChild> UChildCollection { get; protected set; }

      protected global::Testing.UChild _uChildOptional;
      partial void SetUChildOptional(global::Testing.UChild oldValue, ref global::Testing.UChild newValue);
      partial void GetUChildOptional(ref global::Testing.UChild result);

      public virtual global::Testing.UChild UChildOptional
      {
         get
         {
            global::Testing.UChild value = _uChildOptional;
            GetUChildOptional(ref value);
            return (_uChildOptional = value);
         }
         set
         {
            global::Testing.UChild oldValue = _uChildOptional;
            SetUChildOptional(oldValue, ref value);
            if (oldValue != value)
            {
               _uChildOptional = value;
            }
         }
      }

   }
}

