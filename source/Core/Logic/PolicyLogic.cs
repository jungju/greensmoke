using System;
using System.Collections.Generic;

using GreenSmoke.Core.ClientService;
using GreenSmoke.Core.Item;
using GreenSmoke.Core.Policy;


// 로직
namespace GreenSmoke.Core.Logic
{
    public class PolicyLogic : IPolicyInformation,IManagePolicy
    {
        private Dictionary<string, UseChildPolicy> _Policies = new Dictionary<string, UseChildPolicy>();

        public Dictionary<string, UseChildPolicy> Policies
        {
            get { return _Policies; }
        }

        private Dictionary<string, Category> _Categories;

        public PolicyLogic(Dictionary<string, Category> categories)
        {
            _Categories = categories;
        }

        #region IPolicyInformation 멤버

        public Dictionary<string, UseChildPolicy> GetPolicyItem()
        {
            return Policies;
        }

        public Dictionary<string, PolicyChild> GetPolicyChildrenItems(string policyID)
        {
            return Policies[policyID].PolicyChildItems;
        }

        #endregion

        #region IManagePolicy 멤버

        public void CreatePolicy(string name, EnumPolicy kindPolicy)
        {
            UseChildPolicy newUseChildPolicy = null;

            switch (kindPolicy)
            {
                case EnumPolicy.Scenario:
                    newUseChildPolicy = new Scenario(name);
                    break;
                case EnumPolicy.Supervision:
                    newUseChildPolicy = new Supervision(name);
                    break;
            }

            Policies.Add(newUseChildPolicy.ID, newUseChildPolicy);
        }

        public void RemovePolicy(string policyID)
        {
            Policies.Remove(policyID);
        }

        public void AddScenarioItem(string scenarioID, string name, DateTime? offTime, DateTime? onTime, TimeSpan? pauseTimeSpan, TimeSpan? playTimeSpan, params string[] categoryItemsID)
        {
            ScenarioItem scenarioItem = new ScenarioItem(name);

            EnumControlTimeMode timeMode = EnumControlTimeMode.None;
            if (offTime != null && onTime != null) timeMode = EnumControlTimeMode.PowerOnAndPowerOff;
            else if (onTime != null) timeMode = EnumControlTimeMode.PowerOn;
            else if (offTime != null) timeMode = EnumControlTimeMode.PowerOnAndPowerOff;
            scenarioItem.CurrentTimeMode = timeMode;
            scenarioItem.OnTime = onTime;
            scenarioItem.OffTime = offTime;

            EnumControlSpanMode spanMode = EnumControlSpanMode.None;
            if (pauseTimeSpan != null && playTimeSpan != null) spanMode = EnumControlSpanMode.PauseTimeAndPlayTime;
            else if (pauseTimeSpan != null) spanMode = EnumControlSpanMode.PauseTime;
            else if (playTimeSpan != null) spanMode = EnumControlSpanMode.PlayTime;
            scenarioItem.CurrentSpanMode = spanMode;
            scenarioItem.PauseTimeSpan = pauseTimeSpan;
            scenarioItem.PlayTimeSpan = playTimeSpan;

            Policies[scenarioID].AddChild(scenarioItem);

            //전달 받은 카테고리아이템들의 번호를 시나리오아이템에 넣는다.
            foreach (string categoryItemID in categoryItemsID)
            {
                Category categoryItem;

                if (_Categories.TryGetValue(categoryItemID, out categoryItem))
                {
                    scenarioItem.AddCategoryItem(categoryItem);
                }
                else if (_Categories.TryGetValue(categoryItemID, out categoryItem))
                {
                    scenarioItem.AddCategoryItem(categoryItem);
                }
                else
                {
                }
            }

            Policies[scenarioID].AddChild(scenarioItem);
        }

        public void AddSupervisionItem(string supervisionID, string name, int criticalValue, TimeSpan runningSpan, EnumCTValueMode valueMode, EnumProcessMode processMode, params string[] categoryItemsID)
        {
            SupervisionItem supervisionItem = new SupervisionItem(name);

            supervisionItem.CTCriticalValue = criticalValue;
            supervisionItem.RunningSpan = runningSpan;
            supervisionItem.CurrentCTValueMode = valueMode;
            supervisionItem.CurrentProcessMode = processMode;

            //전달 받은 카테고리아이템들의 번호를 시나리오아이템에 넣는다.
            foreach (string categoryItemID in categoryItemsID)
            {
                Category categoryItem;

                if (_Categories.TryGetValue(categoryItemID, out categoryItem))
                {
                    supervisionItem.AddCategoryItem(categoryItem);
                }
                else if (_Categories.TryGetValue(categoryItemID, out categoryItem))
                {
                    supervisionItem.AddCategoryItem(categoryItem);
                }
                else
                {
                }
            }

            Policies[supervisionID].PolicyChildItems.Add(supervisionItem.ID, supervisionItem);
        }

        public void RemovePolicyChildItem(string policyID, string policyChildItemID)
        {
            Policies[policyID].PolicyChildItems.Remove(policyChildItemID);
        }

        #endregion
    }
}
