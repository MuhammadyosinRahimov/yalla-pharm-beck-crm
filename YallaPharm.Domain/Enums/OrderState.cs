namespace YallaPharm.Domain.Enums;

public enum OrderState
{
    Undefined = 0,
    Application = 1,
    InSearch = 2,
    WaitingClient = 3,
    Placement = 4,
    Placed = 5,
    ReadyForShipment = 6,
    AcceptedByCourier = 7,
    Received = 8,
    OnTheWay = 9,
    Delivered = 10,
    Canceled = 11,
    Returned = 12,
    Rejection = 13
}
