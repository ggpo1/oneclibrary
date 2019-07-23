using System.Collections.Generic;

namespace OneCLibrary.Models.Enums
{
    public static class OneCEntityTypes
    {
        public static readonly Dictionary<string, string> Types = new Dictionary<string, string>
        {
            { "CATALOG", "Catalog" },
            { "DOCUMENT", "Document" },
            { "DOCUMENT_JOURNAL", "DocumentJournal" },
            { "CONSTANT", "Constant" },
            { "EXCHANGE_PLAN", "ExchangePlan" },
            { "CHART_OF_ACCOUNTS", "ChartOfAccounts" },
            { "CHART_OF_CALCULATION_TYPES", "ChartOfCalculationTypes" },
            { "CHART_OF_CHARACTERISTIC_TYPES", "ChartOfCharacteristicTypes" },
            { "INFORMATION_REGISTER", "InformationRegister" },
            { "ACCUMULATION_REGISTER", "AccumulationRegister" },
            { "CALCULATION_REGISTER", "CalculationRegister" },
            { "ACCOUNTING_REGISTER", "AccountingRegister" },
            { "BUSINESS_PROCESS", "BusinessProcess" },
            { "TASK", "Task" }
        };
    }
}