using Domain.Commons;

namespace Domain.Entities
{
    public class Address : BaseEntity
    {
        public int CustomerId { get; private set; }
        public virtual Customer Customer { get; private set; }

        public string Title { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public string District { get; private set; }
        public string PostalCode { get; private set; }
        public string FullAddress { get; private set; }
        public bool IsDefault { get; private set; } = false;
        public string Phone { get; private set; }
        public Address()
        {
            
        }

        public Address(int customerId, string title, string country, string city, string district, string postalCode, string fullAddress)
        {
            CustomerId = customerId;
            Title = title;
            Country = country;
            City = city;
            District = district;
            PostalCode = postalCode;
            FullAddress = fullAddress;
        }

        public void Update(string title, string country, string city, string district, string postalCode, string fullAddress, string phone)
        {
            Title = title;
            Country = country;
            City = city;
            District = district;
            PostalCode = postalCode;
            FullAddress = fullAddress;
            Phone = phone;
            UpdateDate = DateTime.Now;
        }

        public void SetAsDefault()
        { 
            IsDefault = true;
        }
        public void UnsetDefault()
        { 
            IsDefault = false;
        }
    }
}
