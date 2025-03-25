export function formatStatusToDanish(status: string | undefined): string {
  switch (status) {
    case "Registered":
      return "Registreret";
    case "InTransfer":
      return "Under Overførsel";
    case "Discharged":
      return "Udskrevet";
    default:
      return "Ukendt";
  }
}
