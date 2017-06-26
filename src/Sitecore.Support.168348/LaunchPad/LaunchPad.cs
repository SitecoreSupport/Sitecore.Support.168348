using System;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.LaunchPad.Configuration;
using Sitecore.Shell.Web;
using Sitecore.Xdb.Configuration;

namespace Sitecore.Support.LaunchPad
{
    public class LaunchPad : Sitecore.LaunchPad.LaunchPad
    {
        public override void Initialize()
        {
            //The ShellPage.IsLoggedIn(); was moved to the end, so the reprots are disabled for sure.
            //The code was wrapped in try/catch block just in case. 
            try
            {
                Database database = Context.Database;
                Item item = database.GetItem("{FA046F20-75A5-4C7F-9FEC-FAC6EEAC33D7}");
                Item item2 = database.GetItem("{7431DE4F-C65B-4CBC-B5BA-FD99E1931AE9}");

                if (this.FallBackMessage != null)
                {
                    this.FallBackMessage.Parameters["Text"] = this.FallBackMessage.Item["Text"].Replace("{{version}}",
                        About.GetVersionNumber(false));
                }
                if ((this.Text2 != null) && !XdbSettings.Enabled)
                {
                    this.Text2.DataSource = "{C2F629FF-BD7D-42B4-A247-3380DAE1408C}";
                }
                if ((!LaunchPadSettings.EnablePersonalizedFrames || !XdbSettings.Enabled) ||
                    ((item == null) || (item2 == null)))
                {
                    this.RowPanelTilesWrapper.Parameters["IsVisible"] = "false";
                    this.InteractionChartApp.Placeholder = string.Empty;
                    this.InteractionChartApp.DataSource = "{AFE511E0-249F-47AF-8439-4E3641DAFAB8}";
                    this.CampaignsChartApp.Placeholder = string.Empty;
                    this.CampaignsChartApp.DataSource = "{AFE511E0-249F-47AF-8439-4E3641DAFAB8}";
                }
            }
            catch (Exception e)
            {
                Log.Error("Sitecore Support issues in the Launchpad.Initialize method", e, this);
            }
            ShellPage.IsLoggedIn();
        }
    }
}
