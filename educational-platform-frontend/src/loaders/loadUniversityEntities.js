export const loadUniversityEntities = async () => {
  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}academy/grouped-entities`
    );
    const data = await response.json();
    return { universityEntities: data };
  } catch (error) {
    return {
      error: "Wystąpił błąd na serwerze, spróbuj ponownie za kilka minut",
    };
  }
};
