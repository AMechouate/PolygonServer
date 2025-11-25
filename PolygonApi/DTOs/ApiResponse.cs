namespace PolygonApi.DTOs
{
    public class ApiResponse
    {
        public string Status { get; set; } = "success";
        public string Message { get; set; } = "Daten erfolgreich gespeichert.";
        public object? Details { get; set; }
    }

    public class ErrorResponse
    {
        public string Status { get; set; } = "error";
        public string Message { get; set; } = "Fehler bei der Verarbeitung.";
        public object? Details { get; set; }
    }
}

