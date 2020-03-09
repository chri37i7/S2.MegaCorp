using System;

namespace S2.MegaCorp.Entities
{
    public class Order
    {
        protected int id;
        protected DateTime orderDate;
        protected DateTime shipmentDate;

        public Order(int id, DateTime orderDate, DateTime shipmentDate)
        {
            Id = id;
            OrderDate = orderDate;
            ShipmentDate = shipmentDate;

            (bool datesAreValid, string message) = ValidateDates(orderDate, shipmentDate);
            if(!datesAreValid)
            {
                throw new ArgumentException(message);
            }
        }

        public virtual int Id
        {
            get
            {
                return id;
            }
            set
            {
                (bool isValid, string errorMessage) = ValidateId(value);
                if(isValid)
                {
                    if(id != value)
                    {
                        id = value;
                    } 
                }
                else
                {
                    throw new ArgumentException(errorMessage, nameof(Id));
                }
            }
        }

        public virtual DateTime OrderDate
        {
            get
            {
                return orderDate;
            }
            set
            {
                if(orderDate != default)
                {
                    (bool isValid, string errorMessage) = ValidateOrderDate(value, shipmentDate);
                    if(isValid)
                    {
                        if(orderDate != value)
                        {
                            orderDate = value;
                        }
                    }
                    else
                    {
                        throw new ArgumentException(errorMessage, nameof(OrderDate));
                    }
                }
                else
                {
                    if(orderDate != value)
                    {
                        orderDate = value;
                    }
                }
            }
        }

        public virtual DateTime ShipmentDate
        {
            get
            {
                return shipmentDate;
            }
            set
            {
                if(shipmentDate != default)
                {
                    (bool isValid, string errorMessage) = ValidateDates(orderDate, value);
                    if(isValid)
                    {
                        if(shipmentDate != value)
                        {
                            shipmentDate = value;
                        }
                    }
                    else
                    {
                        throw new ArgumentException(errorMessage, nameof(OrderDate));
                    }
                }
                else
                {
                    if(shipmentDate != value)
                    {
                        shipmentDate = value;
                    }
                }
            }
        }

        public static (bool, string) ValidateId(int id)
        {
            if(id < 0)
            {
                return (false, "The value cannot be lower than 0");
            }
            else
            {
                return (true, string.Empty);
            }
        }

        public static (bool, string) ValidateOrderDate(DateTime orderDate, DateTime shipmentDate)
        {
            if(orderDate < shipmentDate)
            {
                return (false, "The order cannot be after the shipment date");
            }
            else
            {
                return (true, string.Empty);
            }
        }

        public static (bool, string) ValidateDates(DateTime orderDate, DateTime shipmentDate)
        {
            if(shipmentDate > orderDate)
            {
                return (true, string.Empty);
            }
            else
            {
                return (false, "Invalid dates");
            }
        }
    }
}