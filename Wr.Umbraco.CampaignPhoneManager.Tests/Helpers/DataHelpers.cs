﻿using System.Collections.Generic;
using System.Text;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Tests
{
    public static class DataHelpers
    {
        public static string GeneratePhoneManagerTestDataString(CampaignPhoneManagerModel defaultSettings, List<CampaignDetail> phoneNumbers)
        {
            string phoneManager_Start = "<campaignPhoneManager id=\"1152\" key=\"ee64b6fe-21dc-445c-9256-1d5497f91383\" parentID=\"1103\" level=\"2\" creatorID=\"0\" sortOrder=\"5\" createDate=\"2017-11-13T12:20:43\" updateDate=\"2017-11-27T17:38:26\" nodeName=\"Phone manager\" urlName=\"phone-manager\" path=\"-1,1103,1152\" isDoc=\"\" nodeType=\"1151\" creatorName=\"Joe Bloggs\" writerName=\"Joe Bloggs\" writerID=\"0\" template=\"0\" nodeTypeAlias=\"phoneManager\">";
            string phoneManager_End = "</campaignPhoneManager>";
            string phoneNumber_Start = "<campaignDetail id=\"{0}\" key=\"8b0d2b79-f219-47c0-9c44-a6dc9620{0}\" parentID=\"1152\" level=\"3\" creatorID=\"0\" sortOrder=\"0\" createDate=\"2017-11-27T17:37:57\" updateDate=\"2017-11-27T17:37:57\" nodeName=\"Test number\" urlName=\"test-number\" path=\"-1,1103,1152,1201\" isDoc=\"\" nodeType=\"1150\" creatorName=\"Joe Bloggs\" writerName=\"Joe Bloggs\" writerID=\"0\" template=\"0\" nodeTypeAlias=\"phoneNumber\">";
            string phoneNumber_End = "</campaignDetail>";
            int startingId = 1201;

            if (defaultSettings != null && phoneNumbers != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(phoneManager_Start);

                // Add default settings - use null to not show element at all
                sb.Append(FormatProperty(defaultSettings.DefaultPhoneNumber, "<defaultPhoneNumber>{0}</defaultPhoneNumber>"));
                sb.Append(FormatProperty(defaultSettings.DefaultCampaignQueryStringKey, "<defaultCampaignQueryStringKey>{0}</defaultCampaignQueryStringKey>"));
                sb.Append(FormatProperty(defaultSettings.DefaultPersistDurationInDays, "<defaultPersistDurationInDays>{0}</defaultPersistDurationInDays>"));

                foreach (var item in phoneNumbers)
                {
                    sb.AppendFormat(phoneNumber_Start, startingId.ToString());

                    // Add phoneNumber properties
                    sb.Append(FormatProperty(item.PhoneNumber, "<phoneNumber>{0}</phoneNumber>"));
                    sb.Append(FormatProperty(item.DoNotPersistAcrossVisits, "<doNotPersistAcrossVisits>{0}</doNotPersistAcrossVisits>"));
                    sb.Append(FormatProperty(item.PersistDurationOverride, "<persistDurationOverride>{0}</persistDurationOverride>"));
                    sb.Append(FormatProperty(item.Referrer, "<referrer>{0}</referrer>"));
                    sb.Append(FormatProperty(item.CampaignCode, "<campaignCode>{0}</campaignCode>"));
                    sb.Append(FormatProperty(item.EntryPage, "<entryPage>{0}</entryPage>"));
                    sb.Append(FormatProperty(item.OverwritePersistingItem, "<overwritePersistingItem>{0}</overwritePersistingItem>"));
                    sb.Append(FormatProperty(item.AltMarketingCode, "<altMarketingCode>{0}</altMarketingCode>"));
                    sb.Append(FormatProperty(item.PriorityOrder, "<priorityOrder>{0}</priorityOrder>"));
                    sb.Append(FormatProperty(item.UseAltCampaignQueryStringKey, "<useAltCampaignQueryStringKey>{0}</useAltCampaignQueryStringKey>"));

                    sb.Append(phoneNumber_End);
                    startingId++; // increment phoneNumber id
                }
                sb.Append(phoneManager_End);

                return sb.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Format the property automatically
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyValue"></param>
        /// <param name="holder"></param>
        /// <returns></returns>
        private static string FormatProperty<T>(T propertyValue, string holder)
        {
            if (propertyValue != null)
            {
                if (propertyValue.GetType() == typeof(string))
                {
                    string cDataHolder = "<![CDATA[{0}]]>";
                    var value = propertyValue.ToString();
                    if (value != string.Empty) // if there is a value then enclose it in the CDATA holder
                        value = string.Format(cDataHolder, value);

                    return string.Format(holder, value);
                }
                else if (propertyValue.GetType() == typeof(bool))
                {
                    return string.Format(holder, propertyValue.ToString().ToLower());
                }
                return string.Format(holder, propertyValue);
            }
            return string.Empty;
        }

        /// <summary>
        /// Update the xpath string so it works out of the Umbraco helper method
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static string UpdateXpathForTesting(string xpath)
        {
            return xpath.Replace("$ancestorOrSelf", "/");
        }

        /// <summary>
        /// Combine the passed in phone number data and the core Umbraco text content
        /// </summary>
        /// <returns></returns>
        public static string GetContentXml(string _testPhoneManagerData)
        {
            return GetCoreUmbracoConfigTestXml_Header() + _testPhoneManagerData + GetCoreUmbracoConfigTestXml_Footer();
        }

        /// <summary>
        /// Copy of the /App_Data/umbraco.config for testing
        /// </summary>
        /// <returns></returns>
        private static string GetCoreUmbracoConfigTestXml_Header()
        {
            return @"<?xml version=""1.0"" encoding=""utf-8""?>
<!DOCTYPE root [<!ELEMENT contentBase ANY>
<!ATTLIST contentBase id ID #REQUIRED>
<!ELEMENT feature ANY>
<!ATTLIST feature id ID #REQUIRED>
<!ELEMENT home ANY>
<!ATTLIST home id ID #REQUIRED>
<!ELEMENT navigationBase ANY>
<!ATTLIST navigationBase id ID #REQUIRED>
<!ELEMENT blog ANY>
<!ATTLIST blog id ID #REQUIRED>
<!ELEMENT blogpost ANY>
<!ATTLIST blogpost id ID #REQUIRED>
<!ELEMENT contact ANY>
<!ATTLIST contact id ID #REQUIRED>
<!ELEMENT contentPage ANY>
<!ATTLIST contentPage id ID #REQUIRED>
<!ELEMENT people ANY>
<!ATTLIST people id ID #REQUIRED>
<!ELEMENT person ANY>
<!ATTLIST person id ID #REQUIRED>
<!ELEMENT product ANY>
<!ATTLIST product id ID #REQUIRED>
<!ELEMENT products ANY>
<!ATTLIST products id ID #REQUIRED>
<!ELEMENT phoneNumber ANY>
<!ATTLIST phoneNumber id ID #REQUIRED>
<!ELEMENT phoneManager ANY>
<!ATTLIST phoneManager id ID #REQUIRED>
<!ELEMENT PersonalisationGroup ANY>
<!ATTLIST PersonalisationGroup id ID #REQUIRED>
<!ELEMENT PersonalisationGroupsFolder ANY>
<!ATTLIST PersonalisationGroupsFolder id ID #REQUIRED>
]>
<root id=""-1"">
  <home id=""1103"" key=""156f1933-e327-4dce-b665-110d62720d03"" parentID=""-1"" level=""1"" creatorID=""0"" sortOrder=""0"" createDate=""2017-11-10T17:17:12"" updateDate=""2017-11-10T17:17:22"" nodeName=""Home"" urlName=""home"" path=""-1,1103"" isDoc="""" nodeType=""1093"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1064"" nodeTypeAlias=""home"">
    <bodyText><![CDATA[{
  ""name"": ""1 column layout"",
  ""sections"": [
    {
      ""grid"": 12,
      ""rows"": [
        {
          ""name"": ""Full Width"",
          ""areas"": [
            {
              ""grid"": 12,
              ""allowAll"": false,
              ""allowed"": [
                ""media"",
                ""macro"",
                ""embed"",
                ""headline""
              ],
              ""hasConfig"": false,
              ""controls"": [
                {
                  ""value"": {
                    ""macroAlias"": ""latestBlogposts"",
                    ""macroParamsDictionary"": {
                      ""numberOfPosts"": ""3"",
                      ""startNodeId"": ""umb://document/1cb33e0a400a49389547b05a35739b8b""
                    }
                  },
                  ""editor"": {
                    ""alias"": ""macro""
                  },
                  ""active"": false
                }
              ]
            }
          ],
          ""hasConfig"": false,
          ""id"": ""cbb67dcf-dc82-700a-617f-84e754458e6a""
        }
      ]
    }
  ]
}]]></bodyText>
    <sitename><![CDATA[Umbraco Sample Site]]></sitename>
    <colorTheme>37</colorTheme>
    <HeroBackgroundImage><![CDATA[umb://media/3d758b1f24ec47b0a75c225b0444991b]]></HeroBackgroundImage>
    <font>40</font>
    <footerCTACaption><![CDATA[Read All on the Blog]]></footerCTACaption>
    <footerDescription><![CDATA[Curabitur arcu erat, accumsan id imperdiet et, porttitor at sem. Curabitur arcu erat, accumsan id imperdiet et, porttitor at sem. Vivamus suscipit tortor eget felis porttitor volutpat]]></footerDescription>
    <FooterCtalink><![CDATA[umb://document/1cb33e0a400a49389547b05a35739b8b]]></FooterCtalink>
    <footerHeader><![CDATA[Umbraco Demo]]></footerHeader>
    <footerAddress><![CDATA[Umbraco HQ - Unicorn Square - Haubergsvej 1 - 5000 Odense C - Denmark - +45 70 26 11 62]]></footerAddress>
    <heroDescription><![CDATA[Moonfish, steelhead, lamprey southern flounder tadpole fish sculpin bigeye, blue-redstripe danio collared dogfish. Smalleye squaretail goldfish arowana butterflyfish pipefish wolf-herring jewel tetra, shiner; gibberfish red velvetfish. Thornyhead yellowfin pike threadsail ayu cutlassfish.]]></heroDescription>
    <HeroCtalink><![CDATA[umb://document/485343b1d99c4789a676e9b4c98a38d4]]></HeroCtalink>
    <heroCTACaption><![CDATA[Check our products]]></heroCTACaption>
    <heroHeader><![CDATA[Umbraco Demo]]></heroHeader>
    <products id=""1104"" key=""485343b1-d99c-4789-a676-e9b4c98a38d4"" parentID=""1103"" level=""2"" creatorID=""0"" sortOrder=""0"" createDate=""2017-11-10T17:17:12"" updateDate=""2017-11-10T17:17:22"" nodeName=""Products"" urlName=""products"" path=""-1,1103,1104"" isDoc="""" nodeType=""1102"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1069"" nodeTypeAlias=""products"">
      <bodyText><![CDATA[{
  ""name"": ""1 column layout"",
  ""sections"": [
    {
      ""grid"": 12,
      ""rows"": []
    }
  ]
}]]></bodyText>
      <pageTitle><![CDATA[Our Gorgeous Selection]]></pageTitle>
      <umbracoNavihide>0</umbracoNavihide>
      <defaultCurrency><![CDATA[€]]></defaultCurrency>
      <featuredProducts><![CDATA[umb://document/e09253c015204aac802390742b6180dc,umb://document/9c4dffe2201541998576fdf7120c861d,umb://document/dd4011757019487994315403fb7f62d2,umb://document/978b40bce0084a70950baf7f7ebe7281]]></featuredProducts>
      <product id=""1105"" key=""e09253c0-1520-4aac-8023-90742b6180dc"" parentID=""1104"" level=""3"" creatorID=""0"" sortOrder=""0"" createDate=""2017-11-10T17:17:12"" updateDate=""2017-11-10T17:17:22"" nodeName=""Biker Jacket"" urlName=""biker-jacket"" path=""-1,1103,1104,1105"" isDoc="""" nodeType=""1101"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1068"" nodeTypeAlias=""product"">
        <bodyText><![CDATA[{
  ""name"": ""1 column layout"",
  ""sections"": [
    {
      ""grid"": 12,
      ""rows"": []
    }
  ]
}]]></bodyText>
        <features><![CDATA[[{""name"":""Free shipping"",""ncContentTypeAlias"":""feature"",""featureName"":""Free shipping"",""featureDetails"":""Isn't that awesome - you only pay for the product""},{""name"":""1 Day return policy"",""ncContentTypeAlias"":""feature"",""featureName"":""1 Day return policy"",""featureDetails"":""You'll need to make up your mind fast""},{""name"":""100 Years warranty"",""ncContentTypeAlias"":""feature"",""featureName"":""100 Years warranty"",""featureDetails"":""But if you're satisfied it'll last a lifetime""}]]]></features>
        <sku><![CDATA[UMB-BIKER-JACKET]]></sku>
        <productName><![CDATA[Biker Jacket]]></productName>
        <description><![CDATA[Donec rutrum congue leo eget malesuada. Vivamus suscipit tortor eget felis porttitor volutpat.]]></description>
        <photos><![CDATA[umb://media/208abda163b54ba1bc2a3d40fe156bb6]]></photos>
        <price>199</price>
        <category><![CDATA[bingo,clothing]]></category>
      </product>
      <product id=""1106"" key=""cb88aaa9-10a9-42fb-ac7b-e3e993d493f5"" parentID=""1104"" level=""3"" creatorID=""0"" sortOrder=""1"" createDate=""2017-11-10T17:17:12"" updateDate=""2017-11-10T17:17:22"" nodeName=""Tattoo"" urlName=""tattoo"" path=""-1,1103,1104,1106"" isDoc="""" nodeType=""1101"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1068"" nodeTypeAlias=""product"">
        <bodyText><![CDATA[{
  ""name"": ""1 column layout"",
  ""sections"": [
    {
      ""grid"": 12,
      ""rows"": []
    }
  ]
}]]></bodyText>
        <sku><![CDATA[UMB-TATTOO]]></sku>
        <productName><![CDATA[Tattoo]]></productName>
        <description><![CDATA[Cras ultricies ligula sed magna dictum porta.]]></description>
        <photos><![CDATA[umb://media/ce075369df3d426f972a36b355fd535f]]></photos>
        <price>499</price>
        <category><![CDATA[tattoo,dedication]]></category>
      </product>
      <product id=""1107"" key=""9c4dffe2-2015-4199-8576-fdf7120c861d"" parentID=""1104"" level=""3"" creatorID=""0"" sortOrder=""2"" createDate=""2017-11-10T17:17:12"" updateDate=""2017-11-10T17:17:22"" nodeName=""Unicorn"" urlName=""unicorn"" path=""-1,1103,1104,1107"" isDoc="""" nodeType=""1101"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1068"" nodeTypeAlias=""product"">
        <bodyText><![CDATA[{
  ""name"": ""1 column layout"",
  ""sections"": [
    {
      ""grid"": 12,
      ""rows"": []
    }
  ]
}]]></bodyText>
        <sku><![CDATA[UMB-UNICORN]]></sku>
        <productName><![CDATA[Unicorn]]></productName>
        <description><![CDATA[Quisque velit nisi, pretium ut lacinia in, elementum id enim. Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Cras ultricies ligula sed magna dictum porta.]]></description>
        <photos><![CDATA[umb://media/7239dc966ea1418e9a6e4caa9a8014fa]]></photos>
        <price>249</price>
        <category><![CDATA[animals]]></category>
      </product>
      <product id=""1108"" key=""dd401175-7019-4879-9431-5403fb7f62d2"" parentID=""1104"" level=""3"" creatorID=""0"" sortOrder=""3"" createDate=""2017-11-10T17:17:12"" updateDate=""2017-11-10T17:17:22"" nodeName=""Ping Pong Ball"" urlName=""ping-pong-ball"" path=""-1,1103,1104,1108"" isDoc="""" nodeType=""1101"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1068"" nodeTypeAlias=""product"">
        <bodyText><![CDATA[{
  ""name"": ""1 column layout"",
  ""sections"": [
    {
      ""grid"": 12,
      ""rows"": []
    }
  ]
}]]></bodyText>
        <sku><![CDATA[UMB-PINGPONG]]></sku>
        <productName><![CDATA[Ping Pong Ball]]></productName>
        <description><![CDATA[Vivamus suscipit tortor eget felis porttitor volutpat. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Cras ultricies ligula sed magna dictum porta.]]></description>
        <photos><![CDATA[umb://media/57643398d9174b93bc71e9c3038a0abc]]></photos>
        <price>2</price>
        <category><![CDATA[sports,bingo]]></category>
      </product>
      <product id=""1109"" key=""300c30c1-ed8f-4943-af8b-803eaa8bbeef"" parentID=""1104"" level=""3"" creatorID=""0"" sortOrder=""4"" createDate=""2017-11-10T17:17:12"" updateDate=""2017-11-10T17:17:22"" nodeName=""Bowling Ball"" urlName=""bowling-ball"" path=""-1,1103,1104,1109"" isDoc="""" nodeType=""1101"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1068"" nodeTypeAlias=""product"">
        <bodyText><![CDATA[{
  ""name"": ""1 column layout"",
  ""sections"": [
    {
      ""grid"": 12,
      ""rows"": []
    }
  ]
}]]></bodyText>
        <sku><![CDATA[UMB-BOWLING]]></sku>
        <productName><![CDATA[Bowling Ball]]></productName>
        <description><![CDATA[Cras ultricies ligula sed magna dictum porta.]]></description>
        <photos><![CDATA[umb://media/dbce2b92a00a4aa29e16ca9bb4c2c712]]></photos>
        <price>899</price>
        <category><![CDATA[sports,bingo]]></category>
      </product>
      <product id=""1110"" key=""978b40bc-e008-4a70-950b-af7f7ebe7281"" parentID=""1104"" level=""3"" creatorID=""0"" sortOrder=""5"" createDate=""2017-11-10T17:17:12"" updateDate=""2017-11-10T17:17:22"" nodeName=""Jumpsuit"" urlName=""jumpsuit"" path=""-1,1103,1104,1110"" isDoc="""" nodeType=""1101"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1068"" nodeTypeAlias=""product"">
        <bodyText><![CDATA[{
  ""name"": ""1 column layout"",
  ""sections"": [
    {
      ""grid"": 12,
      ""rows"": []
    }
  ]
}]]></bodyText>
        <sku><![CDATA[UMB-JUMPSUIT]]></sku>
        <productName><![CDATA[Jumpsuit]]></productName>
        <description><![CDATA[Proin eget tortor risus. Vestibulum ac diam sit amet quam vehicula elementum sed sit amet dui. Quisque velit nisi, pretium ut lacinia in, elementum id enim. Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Cras ultricies ligula sed magna dictum porta.]]></description>
        <photos><![CDATA[umb://media/e415f0b75dcd4a99ab9982eba3a1cc00]]></photos>
        <price>89</price>
        <category><![CDATA[fashion,bingo]]></category>
      </product>
      <product id=""1111"" key=""26797b92-8186-4ac4-af7f-9f6ae4aad4f7"" parentID=""1104"" level=""3"" creatorID=""0"" sortOrder=""6"" createDate=""2017-11-10T17:17:13"" updateDate=""2017-11-10T17:17:22"" nodeName=""Banjo"" urlName=""banjo"" path=""-1,1103,1104,1111"" isDoc="""" nodeType=""1101"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1068"" nodeTypeAlias=""product"">
        <bodyText><![CDATA[{
  ""name"": ""1 column layout"",
  ""sections"": [
    {
      ""grid"": 12,
      ""rows"": []
    }
  ]
}]]></bodyText>
        <sku><![CDATA[UMB-BANJO]]></sku>
        <productName><![CDATA[Banjo]]></productName>
        <description><![CDATA[Vivamus suscipit tortor eget felis porttitor volutpat. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec velit neque, auctor sit amet aliquam vel, ullamcorper sit amet ligula. Proin eget tortor risus.]]></description>
        <photos><![CDATA[umb://media/10d1d7ab3774482fb621a6ef396104bc]]></photos>
        <price>399</price>
        <category><![CDATA[bingo,music]]></category>
      </product>
      <product id=""1112"" key=""dab8bdbc-5b36-481d-9244-95d17af7b98a"" parentID=""1104"" level=""3"" creatorID=""0"" sortOrder=""7"" createDate=""2017-11-10T17:17:13"" updateDate=""2017-11-10T17:17:22"" nodeName=""Knitted West"" urlName=""knitted-west"" path=""-1,1103,1104,1112"" isDoc="""" nodeType=""1101"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1068"" nodeTypeAlias=""product"">
        <bodyText><![CDATA[{
  ""name"": ""1 column layout"",
  ""sections"": [
    {
      ""grid"": 12,
      ""rows"": []
    }
  ]
}]]></bodyText>
        <sku><![CDATA[UMB-WEST]]></sku>
        <productName><![CDATA[Knitted Unicorn West]]></productName>
        <description><![CDATA[Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Cras ultricies ligula sed magna dictum porta.]]></description>
        <photos><![CDATA[umb://media/f450c238accb42b39532473407359a24]]></photos>
        <price>1899</price>
        <category><![CDATA[bingo,fashion]]></category>
      </product>
    </products>
    <people id=""1113"" key=""5582ae2f-efa8-422f-a9c9-7ff1c9e8dd85"" parentID=""1103"" level=""2"" creatorID=""0"" sortOrder=""1"" createDate=""2017-11-10T17:17:13"" updateDate=""2017-11-10T17:17:22"" nodeName=""People"" urlName=""people"" path=""-1,1103,1113"" isDoc="""" nodeType=""1099"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1066"" nodeTypeAlias=""people"">
      <bodyText><![CDATA[{""name"":""1 column layout"",""sections"":[{""grid"":""12"",""rows"":[]}]}]]></bodyText>
      <pageTitle><![CDATA[Nice crazy people]]></pageTitle>
      <umbracoNavihide>0</umbracoNavihide>
      <person id=""1114"" key=""9acdfbe7-bfe7-4acc-9d75-b2229ece935b"" parentID=""1113"" level=""3"" creatorID=""0"" sortOrder=""0"" createDate=""2017-11-10T17:17:13"" updateDate=""2017-11-10T17:17:22"" nodeName=""Jan Skovgaard"" urlName=""jan-skovgaard"" path=""-1,1103,1113,1114"" isDoc="""" nodeType=""1100"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1067"" nodeTypeAlias=""person"">
        <umbracoNavihide>0</umbracoNavihide>
        <department><![CDATA[mvp,Denmark]]></department>
        <photo><![CDATA[umb://media/c28e5b4b54af44d89eb97eff7253f546]]></photo>
      </person>
      <person id=""1115"" key=""23dca8e9-d496-4507-8726-dc06ecc5962f"" parentID=""1113"" level=""3"" creatorID=""0"" sortOrder=""1"" createDate=""2017-11-10T17:17:13"" updateDate=""2017-11-10T17:17:22"" nodeName=""Matt Brailsford"" urlName=""matt-brailsford"" path=""-1,1103,1113,1115"" isDoc="""" nodeType=""1100"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1067"" nodeTypeAlias=""person"">
        <umbracoNavihide>0</umbracoNavihide>
        <department><![CDATA[United Kingdom,mvp]]></department>
        <photo><![CDATA[umb://media/8319cfa9910a41008064a1c3b648cb60]]></photo>
        <instagramUsername><![CDATA[circuitbeard]]></instagramUsername>
        <twitterUsername><![CDATA[mattbrailsford]]></twitterUsername>
      </person>
      <person id=""1116"" key=""bff7b1f1-cc49-4bda-8699-ecb85f18bc83"" parentID=""1113"" level=""3"" creatorID=""0"" sortOrder=""2"" createDate=""2017-11-10T17:17:13"" updateDate=""2017-11-10T17:17:22"" nodeName=""Lee Kelleher"" urlName=""lee-kelleher"" path=""-1,1103,1113,1116"" isDoc="""" nodeType=""1100"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1067"" nodeTypeAlias=""person"">
        <umbracoNavihide>0</umbracoNavihide>
        <department><![CDATA[United Kingdom,mvp]]></department>
        <photo><![CDATA[umb://media/def8b9622107486db2d65be3639a6c31]]></photo>
      </person>
      <person id=""1117"" key=""bb6f3ea1-6f18-4a27-a463-714822d36032"" parentID=""1113"" level=""3"" creatorID=""0"" sortOrder=""3"" createDate=""2017-11-10T17:17:13"" updateDate=""2017-11-10T17:17:22"" nodeName=""Jeavon Leopold"" urlName=""jeavon-leopold"" path=""-1,1103,1113,1117"" isDoc="""" nodeType=""1100"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1067"" nodeTypeAlias=""person"">
        <umbracoNavihide>0</umbracoNavihide>
        <department><![CDATA[United Kingdom,mvp]]></department>
        <photo><![CDATA[umb://media/981014a4f0b946dbaa9187cf2027f6e0]]></photo>
      </person>
      <person id=""1118"" key=""44c58a9d-b60d-4001-b9f3-d7783000caa0"" parentID=""1113"" level=""3"" creatorID=""0"" sortOrder=""4"" createDate=""2017-11-10T17:17:13"" updateDate=""2017-11-10T17:17:22"" nodeName=""Jeroen Breuer"" urlName=""jeroen-breuer"" path=""-1,1103,1113,1118"" isDoc="""" nodeType=""1100"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1067"" nodeTypeAlias=""person"">
        <umbracoNavihide>0</umbracoNavihide>
        <department><![CDATA[Netherlands,mvp]]></department>
        <photo><![CDATA[umb://media/fcc186008f9b499589543cb0d335faf1]]></photo>
      </person>
    </people>
    <contentPage id=""1119"" key=""d62f0f1d-e4a9-4093-94ae-4efce18872ee"" parentID=""1103"" level=""2"" creatorID=""0"" sortOrder=""2"" createDate=""2017-11-10T17:17:13"" updateDate=""2017-11-10T17:17:22"" nodeName=""About Us"" urlName=""about-us"" path=""-1,1103,1119"" isDoc="""" nodeType=""1098"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1063"" nodeTypeAlias=""contentPage"">
      <bodyText><![CDATA[{
  ""name"": ""1 column layout"",
  ""sections"": [
    {
      ""grid"": 12,
      ""rows"": [
        {
          ""name"": ""Full Width"",
          ""areas"": [
            {
              ""grid"": 12,
              ""allowAll"": false,
              ""allowed"": [
                ""media"",
                ""embed"",
                ""headline"",
                ""rte"",
                ""macro""
              ],
              ""config"": null,
              ""styles"": null,
              ""hasConfig"": false,
              ""controls"": [
                {
                  ""value"": ""Oooh la la"",
                  ""editor"": {
                    ""alias"": ""headline""
                  },
                  ""styles"": null,
                  ""config"": null
                }
              ]
            }
          ],
          ""styles"": null,
          ""config"": null,
          ""hasConfig"": false,
          ""id"": ""295c0639-aea7-219f-3f3c-bb2e7fcd099c""
        },
        {
          ""name"": ""Article"",
          ""areas"": [
            {
              ""grid"": 4,
              ""allowAll"": false,
              ""allowed"": [
                ""quote"",
                ""embed"",
                ""macro"",
                ""media"",
                ""rte""
              ],
              ""config"": null,
              ""styles"": null,
              ""hasConfig"": false,
              ""controls"": [
                {
                  ""value"": {
                    ""focalPoint"": {
                      ""left"": 0.5,
                      ""top"": 0.5
                    },
                    ""id"": 1142,
                    ""image"": ""/media/1002/18095416144_44a566a5f4_h.jpg""
                  },
                  ""editor"": {
                    ""alias"": ""media""
                  },
                  ""styles"": null,
                  ""config"": null
                }
              ]
            },
            {
              ""grid"": 8,
              ""allowAll"": false,
              ""allowed"": [
                ""rte"",
                ""media"",
                ""macro"",
                ""embed"",
                ""headline"",
                ""quote""
              ],
              ""config"": null,
              ""styles"": null,
              ""hasConfig"": false,
              ""controls"": [
                {
                  ""value"": ""<p>Vestibulum ac diam sit amet quam vehicula elementum sed sit amet dui. Curabitur aliquet quam id dui posuere blandit. Vivamus suscipit tortor eget felis porttitor volutpat. Proin eget tortor risus. Sed porttitor lectus nibh. Cras ultricies ligula sed magna dictum porta. Pellentesque in ipsum id orci porta dapibus. Pellentesque in ipsum id orci porta dapibus. Nulla porttitor accumsan tincidunt. Mauris blandit aliquet elit, eget tincidunt nibh pulvinar a.</p>\n<p>Vestibulum ac diam sit amet quam vehicula elementum sed sit amet dui. Curabitur aliquet quam id dui posuere blandit. Vivamus suscipit tortor eget felis porttitor volutpat. Proin eget tortor risus. Sed porttitor lectus nibh. Cras ultricies ligula sed magna dictum porta. Pellentesque in ipsum id orci porta dapibus. Pellentesque in ipsum id orci porta dapibus. Nulla porttitor accumsan tincidunt. Mauris blandit aliquet elit, eget tincidunt nibh pulvinar a.</p>"",
                  ""editor"": {
                    ""alias"": ""rte""
                  },
                  ""styles"": null,
                  ""config"": null
                },
                {
                  ""value"": ""<iframe width=\""360\"" height=\""203\"" src=\""https://www.youtube.com/embed/HPgKSCp_Y_U?feature=oembed\"" frameborder=\""0\"" allowfullscreen></iframe>"",
                  ""editor"": {
                    ""alias"": ""embed""
                  },
                  ""active"": true
                }
              ],
              ""hasActiveChild"": true,
              ""active"": true
            }
          ],
          ""styles"": null,
          ""config"": null,
          ""hasConfig"": false,
          ""id"": ""ea5aec16-412c-26dc-6649-462288d5ad5d"",
          ""hasActiveChild"": true,
          ""active"": true
        }
      ]
    }
  ]
}]]></bodyText>
      <pageTitle><![CDATA[About Us]]></pageTitle>
      <umbracoNavihide>0</umbracoNavihide>
      <contentPage id=""1120"" key=""2b804661-b071-473b-9604-d9004ff341ab"" parentID=""1119"" level=""3"" creatorID=""0"" sortOrder=""0"" createDate=""2017-11-10T17:17:13"" updateDate=""2017-11-10T17:17:22"" nodeName=""About this Starter Kit"" urlName=""about-this-starter-kit"" path=""-1,1103,1119,1120"" isDoc="""" nodeType=""1098"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1063"" nodeTypeAlias=""contentPage"">
        <bodyText><![CDATA[{""name"":""1 column layout"",""sections"":[{""grid"":""12"",""rows"":[]}]}]]></bodyText>
        <pageTitle><![CDATA[About this Starter Kit]]></pageTitle>
        <umbracoNavihide>0</umbracoNavihide>
      </contentPage>
      <contentPage id=""1121"" key=""0fcf0fff-f19e-4df9-b3a5-a5fcd2c460ab"" parentID=""1119"" level=""3"" creatorID=""0"" sortOrder=""1"" createDate=""2017-11-10T17:17:13"" updateDate=""2017-11-10T17:17:22"" nodeName=""Todo list for the Starter Kit"" urlName=""todo-list-for-the-starter-kit"" path=""-1,1103,1119,1121"" isDoc="""" nodeType=""1098"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1063"" nodeTypeAlias=""contentPage"">
        <bodyText><![CDATA[{
  ""name"": ""1 column layout"",
  ""sections"": [
    {
      ""grid"": 12,
      ""rows"": [
        {
          ""name"": ""Full Width"",
          ""areas"": [
            {
              ""grid"": 12,
              ""allowAll"": false,
              ""allowed"": [
                ""media"",
                ""embed"",
                ""headline"",
                ""rte"",
                ""macro""
              ],
              ""config"": null,
              ""styles"": null,
              ""hasConfig"": false,
              ""controls"": [
                {
                  ""value"": ""<p>Here's what could be improved in the Starter Kit so far:</p>\n<p> </p>\n<p>For v1:</p>\n<ul>\n<li>Use a custom grid editor for testimonials</li>\n<li>Integrated Analytics on pages</li>\n<li>Call To Action Button in the grid (with \""Tag Manager\"" integration)</li>\n<li>Macro for fetching products (with friendly grid preview)</li>\n<li>Design Review (polish)</li>\n<li>Verify licenses of photos (Niels)</li>\n</ul>\n<p>For vNext</p>\n<ul>\n<li><span style=\""text-decoration: line-through;\"">Swap text with uploaded logo</span></li>\n<li>Nicer pickers of products and employees</li>\n<li>Custom Listview for products and employees</li>\n<li>Discus template on blog posts</li>\n<li>404 template</li>\n<li>Member Login/Register/Profile/Forgot password</li>\n<li>Update default styling of grid header</li>\n<li>On a Blog post -&gt; Share/Social (tweet this / facebook this)</li>\n</ul>"",
                  ""editor"": {
                    ""alias"": ""rte""
                  },
                  ""styles"": null,
                  ""config"": null,
                  ""active"": true
                }
              ]
            }
          ],
          ""styles"": null,
          ""config"": null,
          ""hasConfig"": false,
          ""id"": ""74647d9c-958f-8877-8e60-03771deeb7d6""
        }
      ]
    }
  ]
}]]></bodyText>
        <pageTitle><![CDATA[Things to improve]]></pageTitle>
        <umbracoNavihide>0</umbracoNavihide>
      </contentPage>
    </contentPage>
    <blog id=""1122"" key=""1cb33e0a-400a-4938-9547-b05a35739b8b"" parentID=""1103"" level=""2"" creatorID=""0"" sortOrder=""3"" createDate=""2017-11-10T17:17:13"" updateDate=""2017-11-10T17:17:22"" nodeName=""Blog"" urlName=""blog"" path=""-1,1103,1122"" isDoc="""" nodeType=""1095"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1060"" nodeTypeAlias=""blog"">
      <bodyText><![CDATA[{""name"":""1 column layout"",""sections"":[{""grid"":""12"",""rows"":[]}]}]]></bodyText>
      <pageTitle><![CDATA[Behind The Scenes]]></pageTitle>
      <umbracoNavihide>0</umbracoNavihide>
      <howManyPostsShouldBeShown><![CDATA[2]]></howManyPostsShouldBeShown>
      <blogpost id=""1123"" key=""7eedc1b4-b045-4084-8174-9588146ac012"" parentID=""1122"" level=""3"" creatorID=""0"" sortOrder=""0"" createDate=""2017-11-10T17:17:13"" updateDate=""2017-11-10T17:17:22"" nodeName=""My Blog Post"" urlName=""my-blog-post"" path=""-1,1103,1122,1123"" isDoc="""" nodeType=""1096"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1061"" nodeTypeAlias=""blogpost"">
        <umbracoNavihide>0</umbracoNavihide>
        <pageTitle><![CDATA[My Blog Post]]></pageTitle>
        <categories><![CDATA[demo,umbraco,starter kit]]></categories>
        <bodyText><![CDATA[{""name"":""1 column layout"",""sections"":[{""grid"":""12"",""rows"":[{""name"":""Full Width"",""id"":""4dc695d1-336c-0733-399e-0dda19d61c36"",""areas"":[{""grid"":""12"",""controls"":[{""value"":""<p>Curabitur arcu erat, accumsan id imperdiet et, porttitor at sem. Donec sollicitudin molestie malesuada. Vivamus suscipit tortor eget felis porttitor volutpat. Sed porttitor lectus nibh. Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Donec sollicitudin molestie malesuada.</p>\n<p>Curabitur arcu erat, accumsan id imperdiet et, porttitor at sem. Donec sollicitudin molestie malesuada. Vivamus suscipit tortor eget felis porttitor volutpat. Sed porttitor lectus nibh. Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Donec sollicitudin molestie malesuada.</p>\n<p>Curabitur arcu erat, accumsan id imperdiet et, porttitor at sem. Donec sollicitudin molestie malesuada. Vivamus suscipit tortor eget felis porttitor volutpat. Sed porttitor lectus nibh. Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Donec sollicitudin molestie malesuada.</p>"",""editor"":{""alias"":""rte"",""view"":null},""styles"":null,""config"":null}],""styles"":null,""config"":null}],""styles"":null,""config"":null}]}]}]]></bodyText>
        <excerpt><![CDATA[Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla quis lorem ut libero malesuada feugiat. Donec rutrum congue leo eget malesuada. Donec rutrum congue leo eget malesuada.]]></excerpt>
      </blogpost>
      <blogpost id=""1124"" key=""a4174f42-86fb-47ee-a376-c3366597c5fc"" parentID=""1122"" level=""3"" creatorID=""0"" sortOrder=""1"" createDate=""2017-11-10T17:17:13"" updateDate=""2017-11-10T17:17:22"" nodeName=""Another one"" urlName=""another-one"" path=""-1,1103,1122,1124"" isDoc="""" nodeType=""1096"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1061"" nodeTypeAlias=""blogpost"">
        <umbracoNavihide>0</umbracoNavihide>
        <pageTitle><![CDATA[Now it gets exciting]]></pageTitle>
        <categories><![CDATA[cg16,codegarden,umbraco]]></categories>
        <bodyText><![CDATA[{""name"":""1 column layout"",""sections"":[{""grid"":""12"",""rows"":[{""name"":""Article"",""id"":""55820a9f-2fa6-7a03-394d-da36257da59c"",""areas"":[{""grid"":""4"",""controls"":[{""value"":{""focalPoint"":{""left"":0.72576832151300241,""top"":0.38775510204081631},""id"":1131,""image"":""/media/1001/4730684907_8a7f8759cb_b.jpg""},""editor"":{""alias"":""media"",""view"":null},""styles"":null,""config"":null}],""styles"":null,""config"":null},{""grid"":""8"",""controls"":[{""value"":""<p>Donec sollicitudin molestie malesuada. Proin eget tortor risus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Curabitur arcu erat, accumsan id imperdiet et, porttitor at sem. Nulla porttitor accumsan tincidunt. Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Nulla porttitor accumsan tincidunt. Donec rutrum congue leo eget malesuada.</p>\n<p>Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec velit neque, auctor sit amet aliquam vel, ullamcorper sit amet ligula. Pellentesque in ipsum id orci porta dapibus. Donec rutrum congue leo eget malesuada. Nulla porttitor accumsan tincidunt. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec velit neque, auctor sit amet aliquam vel, ullamcorper sit amet ligula. Curabitur non nulla sit amet nisl tempus convallis quis ac lectus. Proin eget tortor risus. Pellentesque in ipsum id orci porta dapibus. Proin eget tortor risus. Sed porttitor lectus nibh.</p>\n<p>Pellentesque in ipsum id orci porta dapibus. Curabitur aliquet quam id dui posuere blandit. Praesent sapien massa, convallis a pellentesque nec, egestas non nisi. Praesent sapien massa, convallis a pellentesque nec, egestas non nisi. Curabitur non nulla sit amet nisl tempus convallis quis ac lectus. Praesent sapien massa, convallis a pellentesque nec, egestas non nisi. Donec rutrum congue leo eget malesuada. Donec rutrum congue leo eget malesuada. Sed porttitor lectus nibh. Nulla quis lorem ut libero malesuada feugiat.</p>"",""editor"":{""alias"":""rte"",""view"":null},""styles"":null,""config"":null}],""styles"":null,""config"":null}],""styles"":null,""config"":null}]}]}]]></bodyText>
        <excerpt><![CDATA[Donec sollicitudin molestie malesuada. Vivamus suscipit tortor eget felis porttitor volutpat. Sed porttitor lectus nibh.]]></excerpt>
      </blogpost>
      <blogpost id=""1125"" key=""09feeca2-a32c-4de0-9c8a-dc19140651c5"" parentID=""1122"" level=""3"" creatorID=""0"" sortOrder=""2"" createDate=""2017-11-10T17:17:13"" updateDate=""2017-11-10T17:17:22"" nodeName=""This will be great"" urlName=""this-will-be-great"" path=""-1,1103,1122,1125"" isDoc="""" nodeType=""1096"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1061"" nodeTypeAlias=""blogpost"">
        <umbracoNavihide>0</umbracoNavihide>
        <pageTitle><![CDATA[This will be great]]></pageTitle>
        <categories><![CDATA[great,umbraco]]></categories>
        <bodyText><![CDATA[{""name"":""1 column layout"",""sections"":[{""grid"":""12"",""rows"":[{""name"":""Full Width"",""id"":""274a2190-82fb-409a-7948-b9c12467e098"",""areas"":[{""grid"":""12"",""controls"":[{""value"":""<p>Vivamus suscipit tortor eget felis porttitor volutpat. Curabitur arcu erat, accumsan id imperdiet et, porttitor at sem. Quisque velit nisi, pretium ut lacinia in, elementum id enim. Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Proin eget tortor risus. Curabitur arcu erat, accumsan id imperdiet et, porttitor at sem. Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Curabitur non nulla sit amet nisl tempus convallis quis ac lectus. Donec rutrum congue leo eget malesuada. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec velit neque, auctor sit amet aliquam vel, ullamcorper sit amet ligula.</p>"",""editor"":{""alias"":""rte"",""view"":null},""styles"":null,""config"":null},{""value"":""<div class=\""umb-loader\"" style=\""height: 10px; margin: 10px 0px;\""></div>"",""editor"":{""alias"":""embed"",""view"":null},""styles"":null,""config"":null},{""value"":""<p> </p>\n<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec sollicitudin molestie malesuada. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Mauris blandit aliquet elit, eget tincidunt nibh pulvinar a. Cras ultricies ligula sed magna dictum porta. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras ultricies ligula sed magna dictum porta. Pellentesque in ipsum id orci porta dapibus.</p>\n<p>Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Nulla quis lorem ut libero malesuada feugiat. Curabitur non nulla sit amet nisl tempus convallis quis ac lectus. Sed porttitor lectus nibh. Vivamus suscipit tortor eget felis porttitor volutpat. Nulla porttitor accumsan tincidunt. Praesent sapien massa, convallis a pellentesque nec, egestas non nisi. Curabitur non nulla sit amet nisl tempus convallis quis ac lectus. Nulla porttitor accumsan tincidunt.</p>\n<p>Vestibulum ac diam sit amet quam vehicula elementum sed sit amet dui. Vivamus suscipit tortor eget felis porttitor volutpat. Sed porttitor lectus nibh. Curabitur non nulla sit amet nisl tempus convallis quis ac lectus. Quisque velit nisi, pretium ut lacinia in, elementum id enim. Donec rutrum congue leo eget malesuada. Nulla porttitor accumsan tincidunt. Nulla quis lorem ut libero malesuada feugiat. Quisque velit nisi, pretium ut lacinia in, elementum id enim. Donec sollicitudin molestie malesuada.</p>\n<p>Proin eget tortor risus. Donec rutrum congue leo eget malesuada. Pellentesque in ipsum id orci porta dapibus. Donec rutrum congue leo eget malesuada. Nulla quis lorem ut libero malesuada feugiat. Curabitur arcu erat, accumsan id imperdiet et, porttitor at sem. Praesent sapien massa, convallis a pellentesque nec, egestas non nisi. Praesent sapien massa, convallis a pellentesque nec, egestas non nisi. Donec sollicitudin molestie malesuada. Vivamus suscipit tortor eget felis porttitor volutpat.</p>"",""editor"":{""alias"":""rte"",""view"":null},""styles"":null,""config"":null}],""styles"":null,""config"":null}],""styles"":null,""config"":null}]}]}]]></bodyText>
        <excerpt><![CDATA[Proin eget tortor risus. Curabitur arcu erat, accumsan id imperdiet et, porttitor at sem. Vivamus magna justo, lacinia eget consectetur sed]]></excerpt>
      </blogpost>
    </blog>
    <contact id=""1126"" key=""71a7e71e-7db2-4b9d-a71c-559b11c683c0"" parentID=""1103"" level=""2"" creatorID=""0"" sortOrder=""4"" createDate=""2017-11-10T17:17:13"" updateDate=""2017-11-10T17:17:22"" nodeName=""Contact"" urlName=""contact"" path=""-1,1103,1126"" isDoc="""" nodeType=""1097"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""1062"" nodeTypeAlias=""contact"">
      <umbracoNavihide>0</umbracoNavihide>
      <contactIntro><![CDATA[<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam eget lacinia nisl. Aenean sollicitudin diam vitae enim ultrices, semper euismod magna efficitur.</p>]]></contactIntro>
      <contactFormHeader><![CDATA[Send Us A Message]]></contactFormHeader>
      <pageTitle><![CDATA[Let's have a chat]]></pageTitle>
      <contactForm><![CDATA[adf160f1-39f5-44c0-b01d-9e2da32bf093]]></contactForm>
      <map><![CDATA[{
  ""zoom"": 13,
  ""position"": {
    ""id"": ""WGS84"",
    ""datum"": ""55.406321,10.387015""
  }
}]]></map>
      <mapHeader><![CDATA[You'll find us here]]></mapHeader>
      <apiKey><![CDATA[AIzaSyBSjIm2tkaskXtivVDbvlXcWkP6JDCoqA4]]></apiKey>
    </contact>
    ";
        }

        private static string GetCoreUmbracoConfigTestXml_Footer()
        {
            return @"
  </home>
  <PersonalisationGroupsFolder id=""1195"" key=""dd6468ba-c0f8-4d64-91e2-8c2555cb0082"" parentID=""-1"" level=""1"" creatorID=""0"" sortOrder=""1"" createDate=""2017-11-21T12:23:46"" updateDate=""2017-11-21T12:23:46"" nodeName=""Personalisation"" urlName=""personalisation"" path=""-1,1195"" isDoc="""" nodeType=""1194"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""0"" nodeTypeAlias=""PersonalisationGroupsFolder"">
    <PersonalisationGroup id=""1196"" key=""fa805413-77c0-4b4b-bf9a-963dfd8d58f7"" parentID=""1195"" level=""2"" creatorID=""0"" sortOrder=""0"" createDate=""2017-11-21T12:37:01"" updateDate=""2017-11-21T12:37:01"" nodeName=""Test 1"" urlName=""test-1"" path=""-1,1195,1196"" isDoc="""" nodeType=""1193"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""0"" nodeTypeAlias=""PersonalisationGroup"">
      <definition><![CDATA[{
  ""match"": ""All"",
  ""duration"": ""Session"",
  ""score"": 50,
  ""details"": [
    {
      ""alias"": ""dayOfWeek"",
      ""definition"": ""[1,2,3,4,5,6,7]""
    }
  ]
}]]></definition>
    </PersonalisationGroup>
    <PersonalisationGroup id=""1197"" key=""fea3b535-ca8a-4084-9758-87f60a8c43c9"" parentID=""1195"" level=""2"" creatorID=""0"" sortOrder=""1"" createDate=""2017-11-21T13:08:57"" updateDate=""2017-11-21T13:08:57"" nodeName=""Test 2"" urlName=""test-2"" path=""-1,1195,1197"" isDoc="""" nodeType=""1193"" creatorName=""Joe Bloggs"" writerName=""Joe Bloggs"" writerID=""0"" template=""0"" nodeTypeAlias=""PersonalisationGroup"">
      <definition><![CDATA[{
  ""match"": ""Any"",
  ""duration"": ""Page"",
  ""score"": 50,
  ""details"": [
    {
      ""alias"": ""referral"",
      ""definition"": ""{ \""value\"": \""google.com\"", \""match\"": \""ContainsValue\"" }""
    }
  ]
}]]></definition>
    </PersonalisationGroup>
  </PersonalisationGroupsFolder>
</root>";
        }
    }

}
