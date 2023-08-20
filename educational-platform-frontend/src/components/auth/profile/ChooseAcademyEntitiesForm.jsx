import { useLoaderData } from "react-router-dom";
import useAcademyEntitiesReset from "../../../hooks/useAcademyEntitiesReset";
import useCurrentAcademyEntitiesOptions from "../../../hooks/useCurrentAcademyEntitiesOptions";
import useInput from "../../../hooks/useInput";

import Select from "../../ui/Select";
import Button from "../../ui/Button";

import { notEmpty } from "../../../utils/validators";

import classes from "./ChooseAcademyEntitiesForm.module.css";

const ChooseAcademyEntitiesForm = () => {
  const loaderData = useLoaderData();
  const { value: universityId, onChange: onUniversityIdChange } =
    useInput(notEmpty);

  const {
    value: facultyId,
    onChange: onFacultyIdChange,

    reset: resetFacultyId,
  } = useInput(notEmpty);

  const {
    value: subjectId,
    onChange: onSubjectIdChange,

    reset: resetSubjectId,
  } = useInput(notEmpty);

  useAcademyEntitiesReset(
    null,
    resetSubjectId,
    resetFacultyId,
    universityId,
    facultyId
  );

  const { universityOptions, facultyOptions, subjectOptions } =
    useCurrentAcademyEntitiesOptions(
      loaderData.universityEntities,
      universityId,
      facultyId
    );

  const onSubmit = async (e) => {
    e.preventDefault();
    console.log(universityId);
    console.log(facultyId);
    console.log(subjectId);
  };

  return (
    <form className={classes.form} onSubmit={onSubmit}>
      <Select
        id="universityId"
        name="universityId"
        label="Uczelnia"
        options={universityOptions}
        onChange={onUniversityIdChange}
        value={universityId}
      />
      <Select
        id="facultyId"
        name="facultyId"
        label="Wydział"
        options={facultyOptions}
        onChange={onFacultyIdChange}
        value={facultyId}
        disabled={!universityId}
      />
      <Select
        id="subjectId"
        name="subjectId"
        label="Kierunek"
        options={subjectOptions}
        onChange={onSubjectIdChange}
        value={subjectId}
        disabled={!universityId || !facultyId}
      />
      <div className={classes.actions}>
        <Button>Zapisz</Button>
      </div>
    </form>
  );
};

export default ChooseAcademyEntitiesForm;
