import { describe, it, expect, vi, Mock } from "vitest";
import { renderWithoutRouter, act } from "../test/test-utils.tsx";
import { BedWard } from "./BedWard";
import * as api from "../api/bedWardPatients";

vi.mock("../api/bedWardPatients");

const mockFetchBedWardPatients = api.fetchBedWardPatients as Mock;

describe("BedWard", () => {
  it("should match snapshot", async () => {
    const mockPatients = [
      {
        patientId: "1",
        givenNames: "John",
        familyName: "Doe",
        personalIdentifier: "123456",
        roomNumber: "101",
      },
    ];

    mockFetchBedWardPatients.mockResolvedValueOnce(mockPatients);

    let container: HTMLElement;
    await act(async () => {
      const result = renderWithoutRouter(<BedWard />);
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
        personalIdentifier: "123456",
        roomNumber: "101",
      },
    ];

    mockFetchBedWardPatients.mockResolvedValueOnce(mockPatients);

    let container: HTMLElement;
    await act(async () => {
      const result = renderWithoutRouter(<BedWard />);
      container = result.container;
    });

    expect(container!.textContent).toContain("John");
    expect(container!.textContent).toContain("Doe");
    expect(container!.textContent).toContain("123456");
  });
});
