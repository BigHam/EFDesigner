﻿  
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ExtensionEnablement = global::Microsoft.VisualStudio.Modeling.Shell.ExtensionEnablement;
using DesignerExtensionEnablement = global::Sawczyn.EFDesigner.EFModel.ExtensionEnablement;

namespace Sawczyn.EFDesigner.EFModel
{
	/// <summary>
	/// Part of the implementation of the PackageBase that deals with extensions
	/// </summary>
	internal partial class EFModelPackageBase
	{
  		private ExtensionEnablement::CommandExtensionRegistrar commandExtensionRegistrar;

		/// <summary>
		/// Initialize Extension Registrars. 
		/// </summary>
		partial void InitializeExtensions()
 		{ 			
			global::System.ComponentModel.Composition.ICompositionService compositionService = global::Microsoft.VisualStudio.Modeling.Shell.ModelingCompositionContainer.CompositionService;
			if (compositionService != null)
			{
				if (this.CommandExtensionRegistrar != null)
				{
					compositionService.SatisfyImportsOnce(global::System.ComponentModel.Composition.AttributedModelServices.CreatePart(this.CommandExtensionRegistrar));
					this.CommandExtensionRegistrar.Initialize(this);
				}
			}
		}

		/// <summary>
		/// Creates the Command Extension Registrar to be used by this DslPackage.
		/// </summary>
		/// <remarks>
		/// It is registered with the MEF container in the InitializeExtensions call.
		/// </remarks>
		protected virtual ExtensionEnablement::CommandExtensionRegistrar CommandExtensionRegistrar
		{
			get 
			{
				if (this.commandExtensionRegistrar == null)
				{
					this.commandExtensionRegistrar = new DesignerExtensionEnablement::EFModelCommandExtensionRegistrar();
				}

				return this.commandExtensionRegistrar;
			}
		}
 	}
}