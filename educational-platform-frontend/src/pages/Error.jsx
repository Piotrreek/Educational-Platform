import Container from "../components/ui/Container";

const Error = () => {
  return (
    <Container>
      <p style={{ fontSize: "36px" }}>
        Wystąpił nieoczekiwany błąd na serwerze. Spróbuj ponownie później.
      </p>
    </Container>
  );
};

export default Error;
