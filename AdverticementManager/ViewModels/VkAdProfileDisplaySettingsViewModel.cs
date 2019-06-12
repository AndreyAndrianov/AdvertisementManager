using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AdvertisementProfiles.VK;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace AdverticementManager.ViewModels
{
    public class VkAdProfileDisplaySettingsViewModel
    {
        public VkAdProfileDisplaySettingsViewModel() : this(ProfileType.General)
        {}

        public VkAdProfileDisplaySettingsViewModel(ProfileType type)
        {
            ProfileType = type;
        }

        public ProfileType ProfileType { get; set; }

        public long AccountId { get; set; }

        [DisplayName("Только активные")]
        public bool OnlyActive { get; set; }

        [DisplayName("Таблица")]
        public DataTableName TableName { get; set; }

        public List<string> GeneralTables { get; } = new List<string> {"Компании", "Кабинет", "Объявления"};

        public List<string> AgencyTables { get; } = new List<string> {"Клиенты"};

        [DisplayName("Данные за")]
        public PeriodItem Period { get; set; }

        public List<string> Periods { get; set; } = new List<string>
        {
            "Последний день", "Последнюю неделю", "Последний месяц", "Все время"
        };

        public IEnumerable<SelectListItem> TableNamesList => ConfigureAvaliableTables();

        public IEnumerable<SelectListItem> PeriodsList => Periods.Select(t => new SelectListItem(t, ConvertPeriodToEnum(t).ToString()));

        private IEnumerable<SelectListItem> ConfigureAvaliableTables()
        {
            var items = GeneralTables.Select(t => new SelectListItem(t, MatchWithEnum(t).ToString()));
            if (ProfileType == ProfileType.Agency)
            {
                items = items.Concat(AgencyTables.Select(t => new SelectListItem(t, MatchWithEnum(t).ToString())));
            }

            return items;
        }

        private PeriodItem ConvertPeriodToEnum(string period)
        {
            switch (period)
            {
                case "Последний день":
                    return PeriodItem.Day;
                case "Последнюю неделю":
                    return PeriodItem.LastWeek;
                case "Последний месяц":
                    return PeriodItem.Month;
                default:
                    return PeriodItem.Overall;
            }
        }

        private DataTableName MatchWithEnum(string s)
        {
            switch (s)
            {
                case "Клиенты":
                    return DataTableName.Client;
                case "Компании":
                    return DataTableName.Campaign;
                case "Объявления":
                    return DataTableName.Ad;
                default:
                    return DataTableName.Office;
            }
        }
    }
}
