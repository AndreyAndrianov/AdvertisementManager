using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace AdverticementManager.ViewModels
{
    public class VkAdProfileDisplaySettingsViewModel
    {
        public int Year { get; set; }

        public string Month { get; set; }

        public int Day { get; set; }

        [DisplayName("Таблица")]
        public DataTableName TableName { get; set; }

        public List<string> AvailableTables { get; } = new List<string> {"Клиенты", "Компании", "Кабинет", "Акции"};

        [DisplayName("Данные за")]
        public PeriodItem Period { get; set; }

        public List<string> Periods { get; set; } = new List<string>
        {
            "Последний день", "Последнюю неделю", "Последний месяц", "Последний год", "Все время"
        };

        public IEnumerable<SelectListItem> TableNamesList => AvailableTables.Select(t => new SelectListItem(t, MatchWithEnum(t).ToString()));
        public IEnumerable<SelectListItem> PeriodsList => Periods.Select(t => new SelectListItem(t, ConvertPeriodToEnum(t).ToString()));

        private PeriodItem ConvertPeriodToEnum(string period)
        {
            switch (period)
            {
                case "Последний день":
                    return PeriodItem.LastDay;
                case "Последнюю неделю":
                    return PeriodItem.LastWeek;
                case "Последний месяц":
                    return PeriodItem.LastMonth;
                case "Последний год":
                    return PeriodItem.LastYear;
                default:
                    return PeriodItem.AllTime;
            }
        }

        private DataTableName MatchWithEnum(string s)
        {
            switch (s)
            {
                case "Клиенты":
                    return DataTableName.Clients;
                case "Компании":
                    return DataTableName.Companies;
                case "Акции":
                    return DataTableName.Ads;
                default:
                    return DataTableName.Office;
            }
        }
    }

    public enum DataTableName
    {
        Office,
        Clients,
        Companies,
        Ads
    }

    public enum PeriodItem
    {
        LastDay,
        LastWeek,
        LastMonth,
        LastYear,
        AllTime
    }
}
