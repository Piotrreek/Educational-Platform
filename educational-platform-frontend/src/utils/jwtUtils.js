import { Buffer } from "buffer";

export const getToken = () => {
  return localStorage.getItem("payload");
};

export const saveToken = (token) => {
  localStorage.setItem("payload", token);
};

export const removeToken = () => {
  localStorage.removeItem("payload");
};

export const retrieveDataFromToken = () => {
  const payload = localStorage.getItem("payload");
  if (!payload) {
    return null;
  }

  const data = JSON.parse(Buffer.from(payload, "base64"));

  return data;
};

export const getRole = () => {
  const data = retrieveDataFromToken();
  console.log(
    data["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
  );
};
