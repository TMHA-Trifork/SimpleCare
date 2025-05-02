import { describe, it, expect, vi, Mock } from "vitest";
import { renderWithRouter, fireEvent, act } from "../test/test-utils";
import EmergencyWard from "./emergencyWard";
import * as api from "../api/emergencyPatients";

vi.mock("../api/emergencyPatients");

const mockFetchEmergencyPatients = api.fetchEmergencyPatients as Mock;
const mockRegisterEmergencyPatient = api.registerEmergencyPatient as Mock;

describe("EmergencyWard", () => {
  it("should match snapshot", async () => {
    const mockPatients = [
      {
        patientId: "1",
        givenNames: "John",
        familyName: "Doe",
        status: "Waiting",
      },
    ];

    mockFetchEmergencyPatients.mockResolvedValueOnce(mockPatients);

    let container: HTMLElement;
    await act(async () => {
      const result = renderWithRouter(<EmergencyWard />);
      container = result.container;
    });

    expect(container!.innerHTML).toMatchSnapshot();
  });

  it("should display patient list", async () => {
    const mockPatients = [
      {
        patientId: "1",
        givenNames: "John",
        familyName: "Doe",
        status: "Waiting",
      },
    ];

    mockFetchEmergencyPatients.mockResolvedValueOnce(mockPatients);

    let container: HTMLElement;
    await act(async () => {
      const result = renderWithRouter(<EmergencyWard />);
      container = result.container;
    });

    expect(container!.textContent).toContain("John");
    expect(container!.textContent).toContain("Doe");
  });

  it("should show registration form when toggle button is clicked", async () => {
    mockFetchEmergencyPatients.mockResolvedValueOnce([]);

    let container: HTMLElement;
    await act(async () => {
      const result = renderWithRouter(<EmergencyWard />);
      container = result.container;
    });

    const toggleButton = container!.querySelector(".register-button-toggle");
    expect(toggleButton).toBeTruthy();

    await act(async () => {
      fireEvent.click(toggleButton!);
    });

    const form = container!.querySelector(".register-form");
    expect(form).toBeTruthy();
  });

  it("should register a new patient", async () => {
    mockFetchEmergencyPatients.mockResolvedValueOnce([]);
    mockRegisterEmergencyPatient.mockResolvedValueOnce({});
    mockFetchEmergencyPatients.mockResolvedValueOnce([]);

    const newPatient = {
      givenNames: "Jane",
      familyName: "Smith",
      personalIdentifier: "123456",
      reason: "Emergency",
    };

    let container: HTMLElement;
    await act(async () => {
      const result = renderWithRouter(<EmergencyWard />);
      container = result.container;
    });

    await act(async () => {
      const toggleButton = container!.querySelector(".register-button-toggle");
      fireEvent.click(toggleButton!);
    });

    await act(async () => {
      const inputs = container!.querySelectorAll("input");
      inputs.forEach((input) => {
        fireEvent.change(input, {
          target: {
            name: input.name,
            value: newPatient[input.name as keyof typeof newPatient],
          },
        });
      });
    });

    await act(async () => {
      const form = container!.querySelector(".register-form");
      fireEvent.submit(form!);
    });

    expect(mockRegisterEmergencyPatient).toHaveBeenCalledWith(newPatient);
  });
});
