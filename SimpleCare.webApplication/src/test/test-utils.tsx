import { ReactElement } from "react";
import { render, RenderOptions } from "@testing-library/react";
import { act } from "react-dom/test-utils";
import { MemoryRouter, Routes, Route } from "react-router-dom";

interface WrapperProps {
  children: React.ReactNode;
}

// Base wrapper without routing
export function BasicWrapper({ children }: WrapperProps) {
  return <>{children}</>;
}

// Wrapper with routing context
export function RouterWrapper({ children }: WrapperProps) {
  return <MemoryRouter>{children}</MemoryRouter>;
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

// Custom render functions
export function renderWithoutRouter(
  ui: ReactElement,
  options?: Omit<RenderOptions, "wrapper">
) {
  return render(ui, { wrapper: BasicWrapper, ...options });
}

export function renderWithRouter(
  ui: ReactElement,
  options?: Omit<RenderOptions, "wrapper">
) {
  return render(ui, { wrapper: RouterWrapper, ...options });
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
// eslint-disable-next-line react-refresh/only-export-components
export * from "@testing-library/react";
export { act };
