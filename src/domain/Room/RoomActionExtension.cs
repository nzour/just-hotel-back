using Domain.User;

namespace Domain.Room
{
    public static class RoomActionExtension
    {
        public static void AddEmployee(this RoomEntity room, UserEntity employee)
        {
            if (UserRole.Employee != employee.Role)
            {
                throw new RoomException($"Room can't has user with role {employee.Role} as an employee.");
            }

            if (room.Employees.Contains(employee))
            {
                throw new RoomException($"Room already has employee with identifier '{employee.Id}'.");
            }

            room.Employees.Add(employee);
        }

        public static void RemoveEmployee(this RoomEntity room, UserEntity employee)
        {
            if (!room.Employees.Contains(employee))
            {
                throw new RoomException($"Room doesn't have employee with identifier '{employee.Id}'.");
            }

            room.Employees.Remove(employee);
        }
    }
}