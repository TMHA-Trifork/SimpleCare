export const API_CONFIG = {
  baseUrl: 'https://localhost:7023'
}

export function getApiUrl(path: string): string {
  return `${API_CONFIG.baseUrl}${path}`;
}
