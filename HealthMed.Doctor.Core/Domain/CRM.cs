using static HealthMed.Core.Exceptions;

namespace HealthMed.Doctor.Core.Domain
{
    public sealed record CRM
    {
        public string Numbers { get; }
        public string State { get; }

        public CRM(string value)
        {
            var numberstring = value.Substring(0, 6);
            var crmString = value.Substring(6, 3);
            var bar = value[9];
            var state = value.Substring(10, 2);

            InvalidCRMException.ThrowIf(
                value.Length > 12 ||
                !int.TryParse(numberstring, out _) ||
                crmString != "CRM" ||
                bar != '/' ||
                !_validStates.Contains(state));

            Numbers = numberstring;
            State = state;
        }

        public override string ToString() => $"{Numbers}CRM/{State}";

        public static implicit operator string(CRM crm) => crm.ToString();

        public static implicit operator CRM(string value) => new CRM(value);

        private readonly string[] _validStates = new string[] 
        { 
            "AC", "AL", "AP", "AM", "BA", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", 
            "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" 
        };
    }
}
