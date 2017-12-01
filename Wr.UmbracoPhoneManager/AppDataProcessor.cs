using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager
{
    /// <summary>
    /// Handles the logic of deciding which inputted data to output and if it should be saved to a cookie (i.e. persisted)
    /// </summary>
    public class AppDataProcessor
    {
        /// <summary>
        /// Model storing the cookie to be saved. It will be null if a cookie is not to be set
        /// </summary>
        public CookieHolder OutputCookieHolder { get; private set; }

        /// <summary>
        /// Model storing the phone data to use. It should always output a result with a phone number, even if it is a placeholder string
        /// </summary>
        public OutputModel OutputModelResult { get; private set; }

        /// <summary>
        /// Output of information on which data source the result came from. For debug purposes
        /// </summary>
        public OutputSource OutputResultSource { get; private set; }

        private readonly DefaultSettings _defaultSettingsFromAdmin;
        private readonly CookieHolder _exisitingCookie;
        private readonly PhoneNumber _foundRecordFromCriteria;

        /// <summary>
        /// Information on which data source the result came from
        /// </summary>
        public enum OutputSource
        {
            ExisitingCookie,
            FoundRecordFromCriteria,
            DefaultNumberFromAdmin,
            LastResortPlaceholder
        }

        public AppDataProcessor(DefaultSettings defaultSettingsFromAdmin, CookieHolder exisitingCookie, PhoneNumber foundRecordFromCriteria)
        {
            _defaultSettingsFromAdmin = defaultSettingsFromAdmin;
            _exisitingCookie = exisitingCookie;
            _foundRecordFromCriteria = foundRecordFromCriteria;

            Main(); // main logic
        }

        /// <summary>
        /// Main logic for selecting the relevant data to output
        /// </summary>
        private void Main()
        {
            // check if there is an exisiting cookie we can use, and check if we want to use it
            if (_exisitingCookie?.Model?.IsValid() ?? false) // we have a valid exisiting cookie
            {
                bool useExisitingCookieForSession = true; // let's assume we will want to use the existing cookie

                if ((_foundRecordFromCriteria?.IsValidToSaveAsCookie() ?? false) && (_foundRecordFromCriteria?.overwritePersistingNumber ?? false)) // foundRecordFromCriteria needs persisting and it should override any exisiting cookie
                {
                    useExisitingCookieForSession = false; // don't use the cookie as the _foundRecordFromCriteria has requested to overwrite any exisiting cookie
                }

                if (useExisitingCookieForSession) // continue using the exisiting cookie - no need to save a newc ookie
                {
                    OutputModelResult = _exisitingCookie.Model; // use the cookie data

                    OutputResultSource = OutputSource.ExisitingCookie;

                    return;
                }
            }

            // check if we have a valid _foundRecordFromCriteria that we can use
            if (_foundRecordFromCriteria?.IsValid() ?? false) // we have a valid foundRecordFromCriteria object that we can use
            {
                OutputModelResult = new OutputModel()
                {
                    PhoneNumber = _foundRecordFromCriteria.phoneNumber,
                    CampaignCode = _foundRecordFromCriteria.campaignCode,
                    AltMarketingCode = _foundRecordFromCriteria.altMarketingCode
                };

                if (_foundRecordFromCriteria.IsValidToSaveAsCookie()) // it is requesting to be persisted via a cookie
                {
                    OutputCookieHolder = new CookieHolder()
                    {
                        Expires = DateTime.Today.AddDays((_foundRecordFromCriteria.persistDurationOverride > 0) ? _foundRecordFromCriteria.persistDurationOverride : _defaultSettingsFromAdmin.DefaultPersistDurationInDays), // persist duration in days - if foundRecord has persistDurationOverride set then use that, otherwise use the default admin setting
                        Model = OutputModelResult
                    };
                }

                OutputResultSource = OutputSource.FoundRecordFromCriteria;

                return;
            }

            // check if a default phone number has been set in the admin system
            if (!string.IsNullOrEmpty(_defaultSettingsFromAdmin?.DefaultPhoneNumber ?? string.Empty))
            {
                OutputModelResult = new OutputModel()
                {
                    PhoneNumber = _defaultSettingsFromAdmin.DefaultPhoneNumber
                };

                OutputResultSource = OutputSource.DefaultNumberFromAdmin;

                return;
            }

            // as a last resort, output placeholder phone number
            OutputModelResult = new OutputModel()
            {
                PhoneNumber = "XXX XXX XXXX"
            };

            OutputResultSource = OutputSource.LastResortPlaceholder;

        }

        /// <summary>
        /// Check if a minimum amount of data is present and can be used
        /// </summary>
        /// <returns>bool</returns>
        public bool HasMinimumValidDataToProcess(bool _forCookie = false)
        {
            if (_exisitingCookie?.Model?.IsValid() ?? false)
                return true;

            if (_forCookie)
            {
                if (_foundRecordFromCriteria?.IsValidToSaveAsCookie() ?? false)
                    return true;
            }
            else
            {
                if (_foundRecordFromCriteria?.IsValid() ?? false)
                    return true;

                if (!string.IsNullOrEmpty(_defaultSettingsFromAdmin?.DefaultPhoneNumber ?? string.Empty))
                    return true;
            }

            return false;
        }
    }
}