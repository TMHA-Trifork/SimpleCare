import { ReactElement } from "react";
import { render, RenderOptions } from "@testing-library/react";
import { act } from "react-dom/test-utils";
import { MemoryRouter, Routes, Route } from "react-router-dom";

interface WrapperProps {
  children: React.ReactNode;
}

// Wrapper for components that need specific routes
interface RouteWrapperProps extends WrapperProps {
  path: string;
  initialEntry?: string;
}

export function RouteWrapper({
  children,
  path,
  initialEntry,
}: RouteWrapperProps) {
  return (
    <MemoryRouter initialEntries={initialEntry ? [initialEntry] : undefined}>
      <Routes>
        <Route path={path} element={children} />
      </Routes>
    </MemoryRouter>
  );
}

export function renderWithRoute(
  ui: ReactElement,
  path: string,
  initialEntry?: string,
  options?: Omit<RenderOptions, "wrapper">
) {
  return render(ui, {
    wrapper: ({ children }) => (
      <RouteWrapper path={path} initialEntry={initialEntry}>
        {children}
      </RouteWrapper>
    ),
    ...options,
  });
}

// Re-export everything from testing-library and add act
export * from "@testing-library/react";
export { act };
