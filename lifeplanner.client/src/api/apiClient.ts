import { useAuth0 } from "@auth0/auth0-react";

export function useApiClient() {
  const { getAccessTokenSilently, isAuthenticated } = useAuth0();

  const apiClient = async <T>(
    endpoint: string,
    options: RequestInit = {},
    handleStatus?: (response: Response) => T | Promise<T>
  ): Promise<T> => {
    const token = isAuthenticated ? await getAccessTokenSilently() : null;

    const response = await fetch(
      `${import.meta.env.VITE_APP_API_URL}${endpoint}`,
      {
        headers: {
          "Content-Type": "application/json",
          ...(token ? { Authorization: `Bearer ${token}` } : {}),
          ...options.headers,
        },
        ...options,
      }
    );

    if (handleStatus && !response.ok) {
      return handleStatus(response);
    }

    if (!response.ok) {
      const error = await response.json().catch(() => ({
        message: response.statusText,
      }));
      throw new Error(error.message || "API request failed");
    }

    return response.json();
  };

  return apiClient;
}
