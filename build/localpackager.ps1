
    tools\UmbracoTools\Wr.UmbracoTools.Packager.exe -set umbraco\phonemanager_settings.json -out "..\..\..\testing\umbracophonemanager-{version}.zip"

    tools\UmbracoTools\Wr.UmbracoTools.Packager.exe -set umbraco\personalisation_plugin_settings.json -out "..\..\..\testing\umbracophonemanager.personalisation-{version}.zip"

	tools\UmbracoTools\Wr.UmbracoTools.Packager.exe -set umbraco\umbformsfieldtypes_settings.json -out "..\..\..\testing\umbracophonemanager.umbformsfieldtypes-{version}.zip"

	tools\nuget.exe pack  ..\Wr.UmbracoPhoneManager\Wr.UmbracoPhoneManager.csproj -OutputDirectory ..\..\..\testing\

	tools\nuget.exe pack  ..\Wr.UmbracoPhoneManager.Personalisation\Wr.UmbracoPhoneManager.Personalisation.csproj -OutputDirectory ..\..\..\testing\

	tools\nuget.exe pack  ..\Wr.UmbracoPhoneManager.UmbFormsFieldTypes\Wr.UmbracoPhoneManager.UmbFormsFieldTypes.csproj -OutputDirectory ..\..\..\testing\
