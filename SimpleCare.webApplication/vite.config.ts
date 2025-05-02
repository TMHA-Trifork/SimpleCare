import { defineConfig } from "vitest/config";

export default defineConfig({
  plugins: [],
  server: {
    host: "localhost",
    port: 3000,
  },
  test: {
    environment: "jsdom",
    globals: true,
    setupFiles: ["./src/test/setup.ts"],
  },
});
