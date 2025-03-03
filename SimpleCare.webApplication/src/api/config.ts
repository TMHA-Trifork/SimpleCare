export const API_CONFIG = {
  baseUrl: 'http://localhost:5034'
}

export function getApiUrl(path: string): string {
  return `${API_CONFIG.baseUrl}${path}`
}
