using RiotSharp;
using RiotSharp.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueOverlay
{
    class RiotConnect
    {
        public void init()
        {
            var api = RiotApi.GetDevelopmentInstance("RGAPI-2b9edc7a-bfcb-442f-ba9d-3b7794c32481");

            
            try
            {
                var summoner = api.GetSummonerByName(Region.euw, "KRO Chimärin");
                var match = api.GetCurrentGame(Region.euw, summoner.Id);
                var teamId = match.Participants.Find(x => x.SummonerId == summoner.Id).TeamId;
                

                var sum = match.Participants.FindAll(x => x.TeamId != teamId);

                foreach(var summoners in sum){
                    var sum1 = summoners.SummonerSpell1;
                    var sum2 = summoners.SummonerSpell2;
                    var champId = summoners.ChampionId;

                  
                    //Add picture to picture bumms
                }
                Debug.WriteLine(summoner.AccountId);
            }
            catch (RiotSharpException e)
            {
                Debug.WriteLine("No user found" + e.ToString());
            }
        }
    }
}
