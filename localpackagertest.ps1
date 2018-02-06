

#(Get-Content Wr.UmbracoPhoneManager\umbraco_package.xml) -replace "<version>(.+)</version>", "<version>2.2.2</version>" | Out-File -Encoding "UTF8" Wr.UmbracoPhoneManager\umbraco_package.xml

 #   (Get-Content Wr.UmbracoPhoneManager.Personalisation\umbraco_package.xml) -replace "<version>(.+)</version>", "<version>3.3.3</version>" | Out-File -Encoding "UTF8" Wr.UmbracoPhoneManager.Personalisation\umbraco_package.xml

    build\tools\UmbracoTools\Wr.UmbracoTools.Packager.exe -set build\umbraco\phonemanager_settings.json -out "..\releases\umbraco\umbracophonemanager-{version}.zip"

    build\tools\UmbracoTools\Wr.UmbracoTools.Packager.exe -set build\umbraco\personalisation_plugin_settings.json -out "..\releases\umbraco\umbracophonemanager.personalisation-{version}.zip"

	nuget.exe pack  Wr.UmbracoPhoneManager\Wr.UmbracoPhoneManager.csproj -OutputDirectory ..\releases\nuget\