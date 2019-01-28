using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RiotSharp;
using RiotSharp.Misc;
using System;
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
        public void init(Form1 form1)
        {
            var api = RiotApi.GetDevelopmentInstance("RGAPI-2b9edc7a-bfcb-442f-ba9d-3b7794c32481");


            try
            {
                var summoner = api.GetSummonerByName(RiotSharp.Misc.Region.euw, "SlimeBoy");
                var match = api.GetCurrentGame(Region.euw, summoner.Id);
                var teamId = match.Participants.Find(x => x.SummonerId == summoner.Id).TeamId;


                var sum = match.Participants.FindAll(x => x.TeamId != teamId);


                //get json for cooldowns
                StreamReader r = new StreamReader("summoner.json");
                string json = r.ReadToEnd();
                var sumsData = JsonConvert.DeserializeObject<QuickType.RiotData>(json);

              


                int i = 0;
                foreach (var summoners in sum)
                {

                    var sum1 = summoners.SummonerSpell1;
                    var sum2 = summoners.SummonerSpell2;
                    var champId = summoners.ChampionId;

                    form1.pb[i].Image = GetSummonnerSpellPicture(sum1);
                    form1.pb[i + 1].Image = GetSummonnerSpellPicture(sum2);

                    long cooldown1 = 0;
                    long cooldown2 = 0;
                    //set cooldown
                    foreach (var sumData in sumsData.Data)
                    {
                        if(sumData.Id == sum1)
                        {
                            cooldown1 = sumData.Cooldown;
                        }

                        if (sumData.Id == sum2)
                        {
                            cooldown2 = sumData.Cooldown;
                        }

                        if(cooldown1 != 0 && cooldown2 != 0)
                        {
                            break;
                        }
                    }

                    //Label setCooldown;
                    //cooldown1 ungerade - cooldown2 gerade
                    form1.lb[i].Text = cooldown1.ToString();
                    form1.lb[i+1].Text = cooldown2.ToString();


                    i = i + 2;
                    Debug.WriteLine("summoner1: " + sum2);
                    //Add picture to picture bumms
                }

            }
            catch (RiotSharpException e)
            {
                Debug.WriteLine("No user found" + e.ToString());
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
    }
}
