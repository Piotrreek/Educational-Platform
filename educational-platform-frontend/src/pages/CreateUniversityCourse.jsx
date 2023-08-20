import CreateUniversityCourseForm from "../components/admin/createAcademyEntities/CreateUniversityCourseForm";
import FormContainer from "../components/admin/createAcademyEntities/FormContainer";
import Header from "../components/auth/Header";

const CreateUniversityCourse = () => {
  return (
    <FormContainer>
      <Header heading="Tworzenie przedmiotu" />
      <CreateUniversityCourseForm />
    </FormContainer>
  );
};

export default CreateUniversityCourse;
