import { useRouteLoaderData } from "react-router-dom";
import { useState } from "react";
import useAcademyEntitiesReset from "../../../hooks/useAcademyEntitiesReset";
import useCurrentAcademyEntitiesOptions from "../../../hooks/useCurrentAcademyEntitiesOptions";
import useInput from "../../../hooks/useInput";

import Select from "../../ui/Select";
import Button from "../../ui/Button";

import { notEmpty } from "../../../utils/validators";
import { getToken } from "../../../utils/jwtUtils";
import { BackendError } from "../../../utils/errors";

import classes from "./ProfileContent.module.css";

const ChooseAcademyEntitiesFormSection = ({ user, setUser }) => {
  const loaderData = useRouteLoaderData("index");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [responseMessage, setResponseMessage] = useState();
  const { value: universityId, onChange: onUniversityIdChange } = useInput(
    notEmpty,
    user.universityId ?? ""
  );

  const {
    value: facultyId,
    onChange: onFacultyIdChange,
    reset: resetFacultyId,
  } = useInput(notEmpty, user.facultyId ?? "");

  const {
    value: subjectId,
    onChange: onSubjectIdChange,
    reset: resetSubjectId,
  } = useInput(notEmpty, user.subjectId ?? "");

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
    setResponseMessage(null);
    e.preventDefault();

    try {
      setIsSubmitting(true);
      const response = await fetch(
        `${process.env.REACT_APP_BACKEND_URL}user/assign-to-academy-entities`,
        {
          method: "POST",
          credentials: "include",
          body: JSON.stringify({
            universityId: !!universityId ? universityId : null,
            facultyId: !!facultyId ? facultyId : null,
            universitySubjectId: !!subjectId ? subjectId : null,
          }),
          headers: {
            Authorization: `Bearer ${getToken()}`,
            "Content-Type": "application/json",
          },
        }
      );

      if (!response.ok) {
        setResponseMessage(BackendError);
        setIsSubmitting(false);
        return;
      }

      setResponseMessage("Zapisano pomyślnie");
      setUser((prev) => {
        return {
          ...prev,
          universityName:
            universityId === ""
              ? ""
              : universityOptions.find((f) => f.value === universityId)?.text,
          facultyName:
            facultyId === ""
              ? ""
              : facultyOptions.find((f) => f.value === facultyId)?.text,
          universitySubjectName:
            subjectId === ""
              ? ""
              : subjectOptions.find((f) => f.value === subjectId)?.text,
          universityId: universityId,
          facultyId: facultyId,
          subjectId: subjectId,
        };
      });
    } catch (_) {}

    setTimeout(() => {
      setResponseMessage(null);
    }, 2000);
    setIsSubmitting(false);
  };

  return (
    <section
      className={`${classes["academy-entities-form-section"]} content__section`}
    >
      <h2>Przypisz się do uczelni, wydziału i kierunku </h2>
      <form onSubmit={onSubmit}>
        {responseMessage && (
          <p className={classes.response}>{responseMessage}</p>
        )}
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
        <div>
          <Button>{isSubmitting ? "Zapisuję..." : "Zapisz"}</Button>
        </div>
      </form>
    </section>
  );
};

export default ChooseAcademyEntitiesFormSection;
