import { getApiUrl } from "./config";

export type RecipientWard = {
  id: string;
  name: string;
};

export async function fetchRecipientWards(): Promise<RecipientWard[]> {
  const response = await fetch(
    getApiUrl("/api/emergency-wards/recipient-wards")
  );
  if (!response.ok) {
    throw new Error("Network response was not ok");
  }
  return response.json();
}
