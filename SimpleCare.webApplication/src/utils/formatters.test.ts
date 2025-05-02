import { describe, it, expect } from "vitest";
import { formatStatusToDanish } from "./formatters";

describe("formatStatusToDanish", () => {
  it("should convert 'Registered' to 'Registreret'", () => {
    expect(formatStatusToDanish("Registered")).toBe("Registreret");
  });

  it("should convert 'InTransfer' to 'Under Overførsel'", () => {
    expect(formatStatusToDanish("InTransfer")).toBe("Under Overførsel");
  });

  it("should convert 'Discharged' to 'Udskrevet'", () => {
    expect(formatStatusToDanish("Discharged")).toBe("Udskrevet");
  });

  it("should return 'Ukendt' for unknown status", () => {
    expect(formatStatusToDanish("NonExistentStatus")).toBe("Ukendt");
  });

  it("should handle undefined input", () => {
    expect(formatStatusToDanish(undefined)).toBe("Ukendt");
  });
});
