
namespace WarehouseManager.Domain.Entities
{
    public sealed class Material
    {
        public Material(
        long id,
        string code,
        string name,
        string unit,
        bool isActive,
        DateTime createdAt,
        DateTime? updatedAt)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException(
                    "Material code is required.",
                    nameof(code));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "Material name is required.",
                    nameof(name));
            }

            if (string.IsNullOrWhiteSpace(unit))
            {
                throw new ArgumentException(
                    "Material unit is required.",
                    nameof(unit));
            }


            Id = id;

            Code = NormalizeCode(code);

            Name = name.Trim();

            Unit = NormalizeUnit(unit);

            IsActive = isActive;

            CreatedAt = createdAt;

            UpdatedAt = updatedAt;
        }

        public long Id { get; }

        public string Code { get; }

        public string Name { get; private set; }

        public string Unit { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; }

        public DateTime? UpdatedAt { get; private set; }

        public static Material CreateNew(
        string code,
        string name,
        string unit)
        {
            return new Material(
                id: 0,
                code: code,
                name: name,
                unit: unit,
                isActive: true,
                createdAt: DateTime.Now,
                updatedAt: null);
        }

        public void UpdateDetails(
            string name,
            string unit)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "Material name is required.",
                    nameof(name));
            }

            if (string.IsNullOrWhiteSpace(unit))
            {
                throw new ArgumentException(
                    "Material unit is required.",
                    nameof(unit));
            }

            Name = name.Trim();

            Unit = NormalizeUnit(unit);

            UpdatedAt = DateTime.Now;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;

            UpdatedAt = DateTime.Now;
        }

        private static string NormalizeCode(
            string value)
        {
            return value
                .Trim()
                .ToUpperInvariant();
        }


        private static string NormalizeUnit(
            string value)
        {
            return value
                .Trim()
                .ToUpperInvariant();
        }
    }
}
