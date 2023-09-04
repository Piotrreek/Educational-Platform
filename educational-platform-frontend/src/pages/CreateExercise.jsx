import Header from "../components/auth/Header";
import FormContainer from "../components/forms/FormContainer";
import CreateExcerciseForm from "../components/forms/CreateExerciseForm";

const CreateExercise = () => {
  return (
    <FormContainer>
      <Header heading="Tworzenie ćwiczenia" />
      <CreateExcerciseForm />
    </FormContainer>
  );
};

export default CreateExercise;
