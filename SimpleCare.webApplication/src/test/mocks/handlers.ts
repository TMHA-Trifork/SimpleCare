import { http, HttpResponse } from "msw";

export const handlers = [
  http.get("*/api/emergency-patients", () => {
    return HttpResponse.json([
      {
        patientId: "1",
        givenNames: "John",
        familyName: "Doe",
        status: "Waiting",
      },
    ]);
  }),

  http.get("*/api/emergency-patients/:id", ({ params }) => {
    const { id } = params;
    return HttpResponse.json({
      patientId: id,
      givenNames: "John",
      familyName: "Doe",
      status: "Waiting",
    });
  }),
];
