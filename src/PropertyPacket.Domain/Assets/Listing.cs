namespace PropertyTenants.Domain.Assets
{
    public abstract class Listing : AbstractDomain
    {
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.UtcNow;
        }
        public string Title { get; private set; }
        public decimal PricePerNight { get; private set; }
        public bool IsAvailable { get; private set; }

        protected Listing(Guid id, string title, decimal pricePerNight) 
        {
            Id = id != Guid.Empty ? id : throw new ArgumentException("Id cannot be empty.", nameof(id));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            PricePerNight = pricePerNight >= 0 ? pricePerNight : throw new ArgumentException("Price cannot be negative.", nameof(pricePerNight));
            IsAvailable = true;
        }

        public void Book()
        {
            if (!IsAvailable)
                throw new InvalidOperationException("Listing is already booked.");
            IsAvailable = false;
            UpdateTimestamp();
        }

        public void MakeAvailable()
        {
            IsAvailable = true;
            UpdateTimestamp();
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(newPrice));
            PricePerNight = newPrice;
            UpdateTimestamp();
        }
    }

    public class Apartment : Listing
    {
        public int FloorNumber { get; private set; }
        public bool HasElevator { get; private set; }

        public Apartment(Guid id, string title, decimal pricePerNight, int floorNumber, bool hasElevator)
            : base(id, title, pricePerNight)
        {
            FloorNumber = floorNumber;
            HasElevator = hasElevator;
        }

        public void UpdateFloorNumber(int newFloorNumber)
        {
            FloorNumber = newFloorNumber;
            UpdateTimestamp();
        }
    }

    public class House : Listing
    {
        public bool HasBackyard { get; private set; }
        public int NumberOfBedrooms { get; private set; }

        public House(Guid id, string title, decimal pricePerNight, bool hasBackyard, int numberOfBedrooms)
            : base(id, title, pricePerNight)
        {
            HasBackyard = hasBackyard;
            NumberOfBedrooms = numberOfBedrooms >= 0 ? numberOfBedrooms : throw new ArgumentException("Number of bedrooms cannot be negative.", nameof(numberOfBedrooms));
        }

        public void UpdateBackyardStatus(bool newHasBackyard)
        {
            HasBackyard = newHasBackyard;
            UpdateTimestamp();
        }
    }

    public class GuestHome : Listing
    {
        public bool IsPrivate { get; private set; }
        public int SquareFootage { get; private set; }

        public GuestHome(Guid id, string title, decimal pricePerNight, bool isPrivate, int squareFootage)
            : base(id, title, pricePerNight)
        {
            IsPrivate = isPrivate;
            SquareFootage = squareFootage >= 0 ? squareFootage : throw new ArgumentException("Square footage cannot be negative.", nameof(squareFootage));
        }

        public void UpdatePrivacy(bool newIsPrivate)
        {
            IsPrivate = newIsPrivate;
            UpdateTimestamp();
        }
    }

    public class UniqueSpace : Listing
    {
        public string UniqueFeature { get; private set; }
        public bool IsPetFriendly { get; private set; }

        public UniqueSpace(Guid id, string title, decimal pricePerNight, string uniqueFeature, bool isPetFriendly)
            : base(id, title, pricePerNight)
        {
            UniqueFeature = uniqueFeature ?? throw new ArgumentNullException(nameof(uniqueFeature));
            IsPetFriendly = isPetFriendly;
        }

        public void UpdateUniqueFeature(string newUniqueFeature)
        {
            UniqueFeature = newUniqueFeature ?? throw new ArgumentNullException(nameof(newUniqueFeature));
            UpdateTimestamp();
        }
    }

    public class Bed : Listing
    {
        public bool IncludesBreakfast { get; private set; }
        public int RoomNumber { get; private set; }

        public Bed(Guid id, string title, decimal pricePerNight, bool includesBreakfast, int roomNumber)
            : base(id, title, pricePerNight)
        {
            IncludesBreakfast = includesBreakfast;
            RoomNumber = roomNumber;
        }

        public void UpdateBreakfastStatus(bool newIncludesBreakfast)
        {
            IncludesBreakfast = newIncludesBreakfast;
            UpdateTimestamp();
        }
    }

    public class Hotel : Listing
    {
        public bool HasPool { get; private set; }
        public int StarRating { get; private set; }

        public Hotel(Guid id, string title, decimal pricePerNight, bool hasPool, int starRating)
            : base(id, title, pricePerNight)
        {
            HasPool = hasPool;
            StarRating = starRating >= 1 && starRating <= 5 ? starRating : throw new ArgumentException("Star rating must be between 1 and 5.", nameof(starRating));
        }

        public void UpdateStarRating(int newStarRating)
        {
            if (newStarRating < 1 || newStarRating > 5)
                throw new ArgumentException("Star rating must be between 1 and 5.", nameof(newStarRating));
            StarRating = newStarRating;
            UpdateTimestamp();
        }
    }
}
