const API_BASE = 'http://localhost:5065/api'

export async function apiFetch<T>(
  path: string,
  options?: RequestInit
): Promise<T> {
  const token = localStorage.getItem('token')

  const response = await fetch(`${API_BASE}${path}`, {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
      ...(options?.headers ?? {})
    }
  })

  if (!response.ok) {
    const text = await response.text()
    throw new Error(text || 'API Error')
  }

  return response.json()
}