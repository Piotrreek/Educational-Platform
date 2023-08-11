import { useEffect, useState } from "react";

const useCurrentAcademyEntitiesOptions = (
  universities,
  universityId,
  facultyId,
  subjectId
) => {
  const [universityOptions, setUniversityOptions] = useState([]);
  const [facultyOptions, setFacultyOptions] = useState([]);
  const [subjectOptions, setSubjectOptions] = useState([]);
  const [courseOptions, setCourseOptions] = useState([]);

  useEffect(() => {
    const currentUniversity = !!universityId
      ? universities.find((e) => e.id === universityId)
      : null;
    const currentFaculty =
      !!currentUniversity && !!facultyId
        ? currentUniversity.faculties.find((f) => f.id === facultyId)
        : null;
    const currentSubject =
      !!currentFaculty && !!subjectId
        ? currentFaculty.universitySubjects.find((f) => f.id === subjectId)
        : null;

    setUniversityOptions([
      { value: "", text: "Wybierz uczelnię" },
      ...universities.map((university) => {
        return {
          value: university.id,
          text: university.name,
        };
      }),
    ]);

    setFacultyOptions([
      { value: "", text: "Wybierz wydział" },
      ...(!!currentUniversity
        ? currentUniversity.faculties.map((faculty) => {
            return {
              value: faculty.id,
              text: faculty.name,
            };
          })
        : []),
    ]);

    setSubjectOptions([
      { value: "", text: "Wybierz wydział" },
      ...(!!currentFaculty
        ? currentFaculty.universitySubjects.map((subject) => {
            return {
              value: subject.id,
              text: subject.name,
            };
          })
        : []),
    ]);

    setCourseOptions([
      { value: "", text: "Wybierz przedmiot" },
      ...(!!currentSubject
        ? currentSubject.universityCourses.map((course) => {
            return {
              value: course.id,
              text: course.name,
            };
          })
        : []),
    ]);
  }, [universities, universityId, facultyId, subjectId]);

  return {
    universityOptions,
    facultyOptions,
    subjectOptions,
    courseOptions,
  };
};

export default useCurrentAcademyEntitiesOptions;
