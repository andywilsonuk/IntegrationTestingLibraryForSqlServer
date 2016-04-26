using System;

namespace IntegrationTestingLibraryForSqlServer
{
    public class DatabaseObjectName
    {
        public DatabaseObjectName(string schemaName, string objectName)
        {
            if (string.IsNullOrWhiteSpace(schemaName)) schemaName = "dbo";
            SchemaName = NormalisedName(schemaName);
            ObjectName = NormalisedName(objectName);
        }
        public string ObjectName { get; private set; }
        public string SchemaName { get; private set; }
        public string Qualified => $"{SchemaName}.{ObjectName}";
        public override string ToString()
        {
            return Qualified;
        }
        public override int GetHashCode()
        {
            return ObjectName.ToLowerInvariant().GetHashCode() ^ SchemaName.ToLowerInvariant().GetHashCode();
        }
        private string NormalisedName(string name)
        {
            string trimmed = name == null ? string.Empty : name.TrimStart('[').TrimEnd(']');
            if (string.IsNullOrWhiteSpace(trimmed)) throw new ValidationException($"The name '{name}' is invalid");
            return $"[{trimmed}]";
        }
        public static DatabaseObjectName FromName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            int index = name.IndexOf('.');
            if (index == -1) return new DatabaseObjectName(null, name);
            return new DatabaseObjectName(name.Substring(0, index), name.Substring(index + 1));
        }
    }
}
