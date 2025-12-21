using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IrishSettings;
using Hicmah.DataSettings;
using Wrappers.WebContext;

namespace SampleWebForms.Settings
{
    public partial class ListSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //HicmahSettingsManager manager = new HicmahSettingsManager(new CacheWrapper());
                HicmahSettings schema = new HicmahSettings();
                IrishSettingsManagerCached manager = new IrishSettingsManagerCached(schema, new CacheWrapper());
                SettingsValues values = manager.Initialize();
                CurrentValues.DataSource = values.Values;
                if (values.Values.Count==0)
                    throw new InvalidOperationException();
                CurrentValues.DataBind();

                ListSettingsSchema.DataSource = schema.Schema.Values;
                ListSettingsSchema.DataBind();


            }
        }
    }
}