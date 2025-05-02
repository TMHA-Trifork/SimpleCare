import { describe, it, expect, vi, beforeEach, afterEach, Mock } from "vitest";
import { createRoot } from "react-dom/client";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import PatientDetails from "./PatientDetails";
import * as api from "../api/emergencyPatients";

vi.mock("../api/emergencyPatients");

const mockFetchEmergencyPatient = api.fetchEmergencyPatient as Mock;

describe("PatientDetails", () => {
  let container: HTMLDivElement;
  let root: ReturnType<typeof createRoot>;

  beforeEach(() => {
    container = document.createElement("div");
    document.body.appendChild(container);
    root = createRoot(container);
  });

  afterEach(() => {
    root.unmount();
    document.body.removeChild(container);
    vi.clearAllMocks();
  });

  it("should match snapshot", async () => {
    mockFetchEmergencyPatient.mockResolvedValueOnce({
      patientId: "1",
      givenNames: "John",
      familyName: "Doe",
    });

    root.render(
      <MemoryRouter initialEntries={["/patient/1"]}>
        <Routes>
          <Route path="/patient/:id" element={<PatientDetails />} />
        </Routes>
      </MemoryRouter>
    );

    await vi.waitFor(() => {
      expect(container.textContent).toContain("John");
      expect(container.textContent).toContain("Doe");
    });

    await vi.waitFor(() => {
      expect(container.innerHTML).toMatchSnapshot();
    });
  });

  it("displays patient details after loading", async () => {
    mockFetchEmergencyPatient.mockResolvedValueOnce({
      patientId: "1",
      givenNames: "John",
      familyName: "Doe",
    });

    root.render(
      <MemoryRouter initialEntries={["/patient/1"]}>
        <Routes>
          <Route path="/patient/:id" element={<PatientDetails />} />
        </Routes>
      </MemoryRouter>
    );

    await vi.waitFor(() => {
      expect(container.textContent).toContain("John");
      expect(container.textContent).toContain("Doe");
    });
  });
});
