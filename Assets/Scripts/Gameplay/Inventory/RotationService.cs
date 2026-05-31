public static class RotationService
{
    public static void Rotate(
        ItemInstance item)
    {
        item.Rotation =
            (RotationState)
            (((int)item.Rotation + 1) % 4);
    }
}