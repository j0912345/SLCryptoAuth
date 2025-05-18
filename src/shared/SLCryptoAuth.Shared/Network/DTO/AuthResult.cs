namespace SLCryptoAuth.Network.DTO;

/// <summary>
/// Виды AuthResult:
/// 1) Обработка через игровой обработчик (оригинальных данных/своих данных)
/// 2) Отправка на сервер запроса со своими данными
/// </summary>
public class AuthResult
{
    /// <summary>
    /// Если true и IsHandled тоже true, то отправляется ответ.
    /// </summary>
    public bool SendAnswer { get; }

    /// <summary>
    /// Если true, заменяет оригинальные данные своими.
    /// </summary>
    public bool ReplaceData { get; }

    /// <summary>
    /// Если false, то пакет обрабатывается игрой.
    /// </summary>
    public bool IsHandled { get; }

    /// <summary>
    /// Данные, которые передаются дальше.
    /// </summary>
    public byte[] Data { get; }

    private AuthResult(bool sendAnswer, bool replaceData, bool isHandled, byte[] data)
    {
        SendAnswer = sendAnswer;
        ReplaceData = replaceData;
        IsHandled = isHandled;
        Data = data;
    }

    /// <summary>
    /// Обработка аутентификации игрой не требуется.
    /// </summary>
    public static AuthResult Handled() => new(false, false, true, []);

    /// <summary>
    /// Требуется обработка аутентификации игрой. Подмена данных.
    /// </summary>
    public static AuthResult HandleViaGame(byte[] data) => new(false, true, false, data);

    /// <summary>
    /// Пакет невалиден, обработка прерывается.
    /// </summary>
    public static AuthResult InvalidPacket(byte[] data) => new(false, false, false, data);

    /// <summary>
    /// Аутентификация требует отправки ответа.
    /// </summary>
    public static AuthResult Answer(byte[] data) => new(true, true, true, data);
}
