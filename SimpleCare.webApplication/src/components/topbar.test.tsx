import { describe, it, expect, vi } from "vitest";
import { renderWithRouter } from "../test/test-utils.tsx";
import TopBar from "./Topbar";

describe("TopBar", () => {
  it("should match snapshot", async () => {
    const { container } = renderWithRouter(<TopBar />);

    await vi.waitFor(() => {
      expect(container.innerHTML).toMatchSnapshot();
    });
  });

  it("should render navigation links", async () => {
    const { container } = renderWithRouter(<TopBar />);

    await vi.waitFor(
      () => {
        const navigationLinks = container.querySelectorAll(
          "nav a, .nav-item a, a[href]"
        );
        expect(navigationLinks.length).toBeGreaterThan(0);
      },
      {
        timeout: 2000,
      }
    );
  });
});
