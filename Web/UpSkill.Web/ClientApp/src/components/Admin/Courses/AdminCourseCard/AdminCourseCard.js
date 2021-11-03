import React from "react";
import "./AdminCourseCard.css";
import GoogleLogo from "../../../../assets/img/courses/Image 2.png";

export default function AdminCoursesCard(props) {
  const {
    courseDetails: { id, title, coachName, price, categoryName },
  } = props;

  return (
    <div className="courses-Card">
      <div
        className="courses-image-wrapper"
        onClick={() => {
          props.getClickedValue(props.courseDetails);
        }}
      >
        <span className="courses-image-title">
          <b>{categoryName}</b>
        </span>
      </div>
      <div className="courses-content d-flex justify-content-between w-75 pt-3">
        <div className="courseInfo">
          <span className="courses-Field">{title}</span>
          <span className="courses-coachPrice">{price}€ per person</span>
        </div>
        <div className="companyAndPriceInfo">
          <span className="coursesfullName">{coachName}</span>
          <h6 className="course-logo">
            <img src={GoogleLogo} alt="logo"></img>
          </h6>
        </div>
      </div>
      <div className="btn-wrapper">{props.children}</div>
    </div>
  );
}