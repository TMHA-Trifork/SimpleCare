import { describe, it, expect, vi, Mock } from "vitest";
import { renderWithRoute, act } from "../test/test-utils.tsx";
import PatientDetails from "./PatientDetails";
import * as api from "../api/emergencyPatients";

vi.mock("../api/emergencyPatients");

const mockFetchEmergencyPatient = api.fetchEmergencyPatient as Mock;

describe("PatientDetails", () => {
  it("should match snapshot", async () => {
    mockFetchEmergencyPatient.mockResolvedValueOnce({
      patientId: "1",
      givenNames: "John",
      familyName: "Doe",
    });

    let container: HTMLElement;

    await act(async () => {
      const result = renderWithRoute(
        <PatientDetails />,
        "/patient/:id",
        "/patient/1"
      );
      container = result.container;
    });

    expect(container!.textContent).toContain("John");
    expect(container!.textContent).toContain("Doe");
    expect(container!.innerHTML).toMatchSnapshot();
  });

  it("displays patient details after loading", async () => {
    mockFetchEmergencyPatient.mockResolvedValueOnce({
      patientId: "1",
      givenNames: "John",
      familyName: "Doe",
    });

    let container: HTMLElement;

    await act(async () => {
      const result = renderWithRoute(
        <PatientDetails />,
        "/patient/:id",
        "/patient/1"
      );
      container = result.container;
    });

    expect(container!.textContent).toContain("John");
    expect(container!.textContent).toContain("Doe");
  });
});
