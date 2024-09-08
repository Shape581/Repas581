using Life;
using Life.BizSystem;
using Life.InventorySystem;
using Life.Network;
using ModKit.Helper;
using ModKit.Helper.JobHelper;
using ModKit.Interfaces;
using ModKit.Internal;
using ModKit.Utils;
using System.Collections.Generic;
using _menu = AAMenu.Menu;

namespace Repas581
{
    public class Repas581 : ModKit.ModKit
    {
        private CustomActivity _customActivity;
        private string amount;

        public Repas581(IGameAPI api) : base(api)
        {
            PluginInformations = new PluginInformations(AssemblyHelper.GetName(), "1.0.0", "Shape581");
        }

        public override void OnPluginInit()
        {
            base.OnPluginInit();
            Logger.LogSuccess($"{PluginInformations.SourceName} v{PluginInformations.Version}", "initialisé");
            InsertMenu();
        }

        //Repas de prisonier
        public void InsertMenu()
        {
            _menu.AddBizTabLine(PluginInformations, new List<Activity.Type> { Activity.Type.LawEnforcement }, null, "(40€) Préparer un repas", (ui) =>
            {
                Player player = PanelHelper.ReturnPlayerFromPanel(ui);
                if (player != null)
                {
                    if (player.serviceMetier == true)
                    {
                        if (player.character.Money >= 40)
                        {
                            InventoryUtils.AddItem(player, 138, 2);
                            InventoryUtils.AddItem(player, 136, 1);

                            player.AddMoney(-40, "Payment");

                            player.Notify("INFORMATION", "Vous avez bien reçu le repas et avez été déduit de l'argent.", NotificationManager.Type.Success);
                        }
                        else
                        {
                            player.Notify("AVERTISSEMENT", "Vous ne possedez pas l'argent sur vous pour faire cela.", NotificationManager.Type.Warning);
                        }
                    }
                    if (player.serviceMetier == false)
                    {
                        player.Notify("AVERTISSEMENT", "Vous n'êtes pas en service métier.", (NotificationManager.Type.Warning));
                    }
                }
            });

            _menu.AddBizTabLine(PluginInformations, new List<Activity.Type> { Activity.Type.Medical }, null, "(40€) Préparer un repas", (ui) =>
            {
                Player player = PanelHelper.ReturnPlayerFromPanel(ui);
                if (player != null)
                {
                    if (player.serviceMetier == true)
                    {
                        if (player.character.Money >= 40)
                        {
                            InventoryUtils.AddItem(player, 138, 2);
                            InventoryUtils.AddItem(player, 136, 1);

                            player.AddMoney(-40, "Payment");

                            player.Notify("SUCCES", "Vous avez bien reçu le repas et avez été déduit de l'argent.", (NotificationManager.Type.Success));
                        }
                        else
                        {
                            player.Notify("AVERTISSEMENT", "Vous ne possedez pas l'argent sur vous pour faire cela.", NotificationManager.Type.Warning);
                        }
                    }
                    else
                    {
                        player.Notify("AVERTISSEMENT", "Vous n'êtes pas en service métier.", (NotificationManager.Type.Warning));
                    }
                }
            });
        }
    }
}