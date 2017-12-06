using System;
using System.Collections.Generic;

namespace esencialAdmin.Data.Models
{
    public partial class Customers : ITrackableEntity
    {
        public Customers()
        {
            Periodes = new HashSet<Periodes>();
            Subscription = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PurchasesRemarks { get; set; }
        public string GeneralRemarks { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }

        public ICollection<Periodes> Periodes { get; set; }
        public ICollection<Subscription> Subscription { get; set; }

        public override bool Equals(object value)
        {
            // Is null?
            if (Object.ReferenceEquals(null, value))
            {
                return false;
            }

            // Is the same object?
            if (Object.ReferenceEquals(this, value))
            {
                return true;
            }

            // Is the same type?
            if (value.GetType() != this.GetType())
            {
                return false;
            }

            return IsEqual((Customers)value);
        }

        public bool Equals(Customers customer)
        {
            // Is null?
            if (Object.ReferenceEquals(null, customer))
            {
                return false;
            }

            // Is the same object?
            if (Object.ReferenceEquals(this, customer))
            {
                return true;
            }

            return IsEqual(customer);
        }

        private bool IsEqual(Customers customer)
        {
            // A pure implementation of value equality that avoids the routine checks above
            // We use String.Equals to really drive home our fear of an improperly overridden "=="
            return String.Equals(FirstName, customer.FirstName)
                && String.Equals(LastName, customer.LastName)
                && String.Equals(Street, customer.Street)
                && String.Equals(Zip, customer.Zip)
                && String.Equals(City, customer.City)
                && String.Equals(Phone, customer.Phone)
                && String.Equals(Email, customer.Email)
                && String.Equals(Company, customer.Company)
                && String.Equals(GeneralRemarks, customer.GeneralRemarks)
                && String.Equals(PurchasesRemarks, customer.PurchasesRemarks)
                && String.Equals(Title, customer.Title)
                && int.Equals(Id, customer.Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, FirstName) ? FirstName.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, LastName) ? LastName.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Street) ? Street.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Zip) ? Zip.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, City) ? City.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Phone) ? Phone.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Email) ? Email.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Company) ? Company.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, GeneralRemarks) ? GeneralRemarks.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, PurchasesRemarks) ? PurchasesRemarks.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Title) ? Title.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Id) ? Id.GetHashCode() : 0);

                return hash;
            }
        }
    }
}
