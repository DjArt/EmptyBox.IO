namespace EmptyBox.IO.Devices.GPIO
{
    /// <summary>
    /// Событие, уведомляющее об изменении напряжения на контакте GPIO.
    /// </summary>
    /// <param name="pin">Контакт, на котором изменилось напряжение.</param>
    /// <param name="edge">Тип произошедшего изменения.</param>
    public delegate void GPIOPinValueChanged(IGPIOPin pin, GPIOPinEdge edge);
}
