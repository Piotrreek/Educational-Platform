import { useCallback, useEffect, useState } from "react";
import {
  Form,
  useActionData,
  useNavigation,
  useRouteLoaderData,
} from "react-router-dom";
import useInput from "../../hooks/useInput";
import useCurrentAcademyEntitiesOptions from "../../hooks/useCurrentAcademyEntitiesOptions";
import useAcademyEntitiesReset from "../../hooks/useAcademyEntitiesReset";

import Input from "../ui/Input";
import Select from "../ui/Select";
import Actions from "../auth/Actions";
import TextArea from "../ui/TextArea";
import Button from "../ui/Button";

import BadRequestMessage from "../auth/BadRequestMessage";

import { notEmpty } from "../../utils/validators";

import formClasses from "../ui/Form.module.css";

const CreateDidacticMaterialForm = () => {
  const loaderData = useRouteLoaderData("index");
  const actionData = useActionData();
  const navigation = useNavigation();

  const isSubmitting = navigation.state === "submitting";
  const error = actionData?.error;

  const [isSuccess, setIsSuccess] = useState(false);

  const {
    value: didacticMaterialType,
    onChange: onDiDacticMaterialTypeChange,
  } = useInput(() => true, "File");

  const isTypeTextValidator = useCallback(
    (value) => {
      if (didacticMaterialType === "Text" && !value) {
        return { isValid: false, error: "To pole nie może być puste" };
      }

      return { isValid: true };
    },
    [didacticMaterialType]
  );

  const isTypeFileValidator = useCallback(
    (value) => {
      if (didacticMaterialType === "File" && !value) {
        return { isValid: false, error: "Plik nie może być pusty" };
      }

      return { isValid: true };
    },
    [didacticMaterialType]
  );

  const {
    value: universityId,
    onChange: onUniversityIdChange,
    onBlur: onUniversityIdBlur,
    hasError: universityIdHasError,
    error: universityIdError,
    isValid: universityIdIsValid,
    reset: resetUniversityId,
  } = useInput(notEmpty);

  const {
    value: facultyId,
    onChange: onFacultyIdChange,
    onBlur: onFacultyIdBlur,
    hasError: facultyIdHasError,
    error: facultyIdError,
    isValid: facultyIdIsValid,
    reset: resetFacultyId,
  } = useInput(notEmpty);

  const {
    value: subjectId,
    onChange: onSubjectIdChange,
    onBlur: onSubjectIdBlur,
    hasError: subjectIdHasError,
    error: subjectIdError,
    isValid: subjectIdIsValid,
    reset: resetSubjectId,
  } = useInput(notEmpty);

  const {
    value: courseId,
    onChange: onCourseIdChange,
    onBlur: onCourseIdBlur,
    hasError: courseIdHasError,
    error: courseIdError,
    isValid: courseIdIsValid,
    reset: resetCourseId,
  } = useInput(notEmpty);

  const {
    value: keywords,
    onChange: onKeywordsChange,
    reset: resetKeywords,
  } = useInput(() => true);
  const {
    value: description,
    onChange: onDescriptionChange,
    reset: resetDescription,
  } = useInput(() => true);

  const {
    value: name,
    onChange: onNameChange,
    onBlur: onNameBlur,
    hasError: nameHasError,
    error: nameError,
    isValid: nameIsValid,
    reset: resetName,
  } = useInput(isTypeTextValidator);

  const {
    value: content,
    onChange: onContentChange,
    onBlur: onContentBlur,
    hasError: contentHasError,
    error: contentError,
    isValid: contentIsValid,
    reset: resetContent,
  } = useInput(isTypeTextValidator);

  const {
    value: file,
    onChange: onFileChange,
    onBlur: onFileBlur,
    hasError: fileHasError,
    error: fileError,
    isValid: fileIsValid,
    reset: resetFile,
  } = useInput(isTypeFileValidator);

  const { universityOptions, facultyOptions, subjectOptions, courseOptions } =
    useCurrentAcademyEntitiesOptions(
      loaderData.universityEntities,
      universityId,
      facultyId,
      subjectId
    );

  useAcademyEntitiesReset(
    resetCourseId,
    resetSubjectId,
    resetFacultyId,
    universityId,
    facultyId,
    subjectId
  );

  useEffect(() => {
    if (!actionData?.isSuccess) {
      return;
    }

    setIsSuccess(true);
    resetCourseId();
    resetSubjectId();
    resetFacultyId();
    resetUniversityId();
    resetKeywords();
    resetDescription();
    resetFile();
    resetName();
    resetContent();

    const timeout = setTimeout(() => {
      setIsSuccess(false);
    }, 5000);

    return () => {
      clearTimeout(timeout);
      setIsSuccess(false);
    };
  }, [
    actionData,
    resetContent,
    resetCourseId,
    resetDescription,
    resetFacultyId,
    resetFile,
    resetKeywords,
    resetName,
    resetSubjectId,
    resetUniversityId,
  ]);

  if (!!loaderData.error) {
    return <p className={formClasses.error}>{loaderData.error}</p>;
  }

  const didacticMaterialTypeOptions = [
    { value: "File", text: "Plik" },
    { value: "Text", text: "Tekst" },
  ];

  const formStateIsValid =
    universityIdIsValid &&
    facultyIdIsValid &&
    subjectIdIsValid &&
    contentIsValid &&
    courseIdIsValid &&
    nameIsValid &&
    fileIsValid;
  return (
    <Form
      className={formClasses.form}
      method="POST"
      encType="multipart/form-data"
      action="/didactic-material/create"
    >
      {error && <BadRequestMessage message={error} />}
      {isSuccess && (
        <p className={formClasses.success}>Pomyślnie utworzono materiał</p>
      )}
      <Select
        id="didacticMaterialType"
        name="didacticMaterialType"
        label="Typ materiału"
        options={didacticMaterialTypeOptions}
        onChange={onDiDacticMaterialTypeChange}
        value={didacticMaterialType}
      />
      <Select
        id="universityId"
        name="universityId"
        label="Uczelnia"
        options={universityOptions}
        onChange={onUniversityIdChange}
        onBlur={onUniversityIdBlur}
        value={universityId}
        hasError={universityIdHasError}
        error={universityIdError}
      />
      <Select
        id="facultyId"
        name="facultyId"
        label="Wydział"
        options={facultyOptions}
        onChange={onFacultyIdChange}
        onBlur={onFacultyIdBlur}
        value={facultyId}
        disabled={!universityId}
        hasError={facultyIdHasError}
        error={facultyIdError}
      />
      <Select
        id="subjectId"
        name="subjectId"
        label="Kierunek"
        options={subjectOptions}
        onChange={onSubjectIdChange}
        onBlur={onSubjectIdBlur}
        value={subjectId}
        disabled={!universityId || !facultyId}
        hasError={subjectIdHasError}
        error={subjectIdError}
      />
      <Select
        id="courseId"
        name="universityCourseId"
        label="Przedmiot"
        options={courseOptions}
        onChange={onCourseIdChange}
        onBlur={onCourseIdBlur}
        value={courseId}
        disabled={!universityId || !facultyId || !subjectId}
        hasError={courseIdHasError}
        error={courseIdError}
      />
      <Input
        id="keywords"
        name="keywords"
        label="Słowa kluczowe"
        type="text"
        value={keywords}
        onChange={onKeywordsChange}
      />
      <TextArea
        id="description"
        name="description"
        label="Opis"
        value={description}
        onChange={onDescriptionChange}
      />
      {didacticMaterialType === "Text" && (
        <>
          <Input
            id="name"
            name="name"
            label="Nazwa"
            type="text"
            value={name}
            onChange={onNameChange}
            onBlur={onNameBlur}
            hasError={nameHasError}
            error={nameError}
          />
          <TextArea
            id="content"
            name="content"
            label="Zawartość"
            value={content}
            onChange={onContentChange}
            onBlur={onContentBlur}
            hasError={contentHasError}
            error={contentError}
          />
        </>
      )}
      {!(didacticMaterialType === "Text") && (
        <Input
          id="file"
          name="file"
          type="file"
          label="Plik"
          value={file}
          onChange={onFileChange}
          onBlur={onFileBlur}
          hasError={fileHasError}
          error={fileError}
        />
      )}
      <Actions className={formClasses["actions-right"]}>
        <Button disabled={!formStateIsValid}>
          {isSubmitting ? "Dodaję..." : "Dodaj"}
        </Button>
      </Actions>
    </Form>
  );
};

export default CreateDidacticMaterialForm;
