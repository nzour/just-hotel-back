using Domain.Room;
using Domain.User;

namespace Domain.RoomEntity
{
    public static class RoomActionExtension
    {
        public static void Rent(this Room.RoomEntity roomEntity, User.UserEntity rentedBy, RentalDates dates)
        {
            RoomException.AssertCanRentRoom(roomEntity, rentedBy);
            RoomException.AssertRentalDatesValid(dates.DateFrom, dates.DateTo);

            roomEntity.RentedBy = rentedBy;
            roomEntity.RentalDates = dates;
        }

        public static void FinishRental(this Room.RoomEntity roomEntity)
        {
            roomEntity.RentedBy = null;
            roomEntity.RentalDates = new RentalDates();
        }

        public static void AddEmployee(this Room.RoomEntity roomEntity, User.UserEntity employee)
        {
            if (UserRole.Employee != employee.Role)
            {
                throw new RoomException($"Room can't has user with role {employee.Role} as an employee.");
            }

            if (roomEntity.Employees.Contains(employee))
            {
                throw new RoomException($"Room already has employee with identifier '{employee.Id}'");
            }

            roomEntity.Employees.Add(employee);
        }

        public static void RemoveEmployee(this Room.RoomEntity roomEntity, User.UserEntity employee)
        {
            if (!roomEntity.Employees.Contains(employee))
            {
                throw new RoomException($"Room doesn't has employee with identifier '{employee.Id}'.");
            }

            roomEntity.Employees.Remove(employee);
        }
    }
}