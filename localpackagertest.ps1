

(Get-Content Wr.UmbracoPhoneManager\umbraco_package.xml) -replace "<version>(.+)</version>", "<version>2.2.2</version>" | Out-File -Encoding "UTF8" Wr.UmbracoPhoneManager\umbraco_package.xml

    (Get-Content Wr.UmbracoPhoneManager.Personalisation\umbraco_package.xml) -replace "<version>(.+)</version>", "<version>3.3.3</version>" | Out-File -Encoding "UTF8" Wr.UmbracoPhoneManager.Personalisation\umbraco_package.xml

    build\tools\UmbracoTools\Wr.UmbracoTools.Packager.exe -set Wr.UmbracoPhoneManager\umbraco_package_settings.json -out "..\releases\umbraco\umbracophonemanager-{version}.zip"

    build\tools\UmbracoTools\Wr.UmbracoTools.Packager.exe -set Wr.UmbracoPhoneManager.Personalisation\umbraco_package_settings.json -out "..\releases\umbraco\umbracophonemanager.personalisation-{version}.zip"

	nuget pack  Wr.UmbracoPhoneManager\Wr.UmbracoPhoneManager.nuspec -OutputDirectory ..\releases\nuget\