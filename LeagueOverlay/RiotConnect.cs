using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RiotNet;
using RiotNet.Models;
using System;
using QuickTypeChamps;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Region = RiotSharp.Misc.Region;

namespace LeagueOverlay
{

    class RiotConnect
    {
        private List<CdLabel> cdTable = new List<CdLabel>();

        public async Task InitAsync(Form1 form1)
        {
            try
            {


                IRiotClient client = new RiotClient(new RiotClientSettings
                {
                    ApiKey = "RGAPI-2b9edc7a-bfcb-442f-ba9d-3b7794c32481"
                });

                Summoner actSummoner = await client.GetSummonerBySummonerNameAsync("Proxyfox", PlatformId.EUW1).ConfigureAwait(false);

                CurrentGameInfo gameInfo = await client.GetActiveGameBySummonerIdAsync(actSummoner.Id, PlatformId.EUW1);
                var teamId = gameInfo.Participants.Find(x => x.SummonerId == actSummoner.Id).TeamId;
                var sum = gameInfo.Participants.FindAll(x => x.TeamId != teamId);

                //get json for cooldowns
                StreamReader r = new StreamReader("summoner.json");
                string json = r.ReadToEnd();
                var sumsData = JsonConvert.DeserializeObject<QuickType.RiotData>(json);

                StreamReader r2 = new StreamReader("champions.json");
                string json2 = r2.ReadToEnd();
                var chamData = JsonConvert.DeserializeObject<RiotChampData>(json2);

                int i = 0;
                foreach (var summoner in sum)
                {
                    var sum1 = summoner.Spell1Id;
                    var sum2 = summoner.Spell2Id;
                    var champId = summoner.ChampionId;

                    form1.pb[i].Image = GetSummonnerSpellPicture(sum1);
                    form1.pb[i + 1].Image = GetSummonnerSpellPicture(sum2);

                    int cooldown1 = 0;
                    int cooldown2 = 0;
                    //set cooldown
                    foreach (var sumData in sumsData.Data)
                    {
                        if (sumData.Id == sum1)
                        {
                            cooldown1 = sumData.Cooldown;
                        }

                        if (sumData.Id == sum2)
                        {
                            cooldown2 = sumData.Cooldown;
                        }

                        if (cooldown1 != 0 && cooldown2 != 0)
                        {
                            break;
                        }
                    }

                    //Label setCooldown

                    form1.lb[i].Invoke((MethodInvoker)delegate ()
                     {
                         form1.lb[i].Text = cooldown1.ToString();
                         form1.lb[i + 1].Text = cooldown2.ToString();
                         form1.championLabel[i / 2].Text = GetChampionLabel(chamData, champId);
                     });



                    cdTable.Add(new CdLabel(form1.lb[i], cooldown1));
                    cdTable.Add(new CdLabel(form1.lb[i + 1], cooldown2));

                    i = i + 2;

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

        }

        private Bitmap GetSummonnerSpellPicture(long sum)
        {
            switch (sum)
            {
                case 21:
                    //Barrier
                    return Properties.Resources.SummonerBarrier;
                case 1:
                    //Cleanse
                    return Properties.Resources.SummonerBoost;
                case 14:
                    //Ignite
                    return Properties.Resources.SummonerDot;
                case 3:
                    //Exhaust
                    return Properties.Resources.SummonerExhaust;
                case 4:
                    //Flash
                    return Properties.Resources.SummonerFlash;
                case 6:
                    //Ghost
                    return Properties.Resources.SummonerHaste;
                case 7:
                    //Heal
                    return Properties.Resources.SummonerHeal;
                case 13:
                    //Clarity
                    return Properties.Resources.SummonerMana;
                case 11:
                    //Smite
                    return Properties.Resources.SummonerSmite;
                case 12:
                    //Teleport
                    return Properties.Resources.SummonerTeleport;
            }
            return null;
        }

        private String GetChampionLabel(RiotChampData champData, long champId)
        {
            foreach (var champ in champData.Data)
            {
                if (champId.Equals(champ.Key))
                {
                    return champ.Id.ToString();
                }
            }
            return null;
        }

        public List<CdLabel> GetCdLabels
        {
            get
            {
                return cdTable;
            }
        }
    }
}
